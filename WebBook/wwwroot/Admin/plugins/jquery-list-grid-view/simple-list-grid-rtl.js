"use strict";
!(function ($, fnName) {
    var debounce = function (func, threshold, execAsap) {
        var timeout;
        return function () {
            function delayed() {
                execAsap || func.apply(obj, args), (timeout = null);
            }
            var obj = this,
                args = arguments;
            timeout ? clearTimeout(timeout) : execAsap && func.apply(obj, args), (timeout = setTimeout(delayed, threshold || 250));
        };
    };
    jQuery.fn[fnName] = function (fn) {
        return fn ? this.bind("resize", debounce(fn)) : this.trigger(fnName);
    };
})(jQuery, "smartresize"),
    (function ($, window, document, undefined) {
    function Plugin(element, options) {
            var that = this;
            (this.element = element),
                (this.$element = $(element)),
                (this.settings = $.extend({}, defaults, options)),
                (this._defaults = defaults),
                (this._name = pluginName),
                (this.initialized = !1),
                (this.body = $("body")),
                (this.list = this.$element.find("ul.list-grid-ul")),
                (this.disabled = !0),
                (this.state = this.settings.state),
                (this.delay = this.settings.delay),
                (this.margin = this.settings.margin),
                (this.height = 0),
                (this.elems = []);
            var toggleContainer = $("<p />", { class: "clearfix" }).insertBefore(this.$element);
            (this.toggle = $("<a />", {
                class: "btn btn-default btn-sm pull-right",
                html: '<i class="fa fa-th" aria-hidden="true"></i>',
                click: function () {
                    that.disabled === !1 && ((that.disabled = !0), (that.state = "list" === that.state ? "grid" : "list"), that.render());
                },
            }).appendTo(toggleContainer)),
                this.init();
        }
        var pluginName = "simpleListGrid",
            defaults = { state: "list", margin: 10, delay: 50 };
        $.extend(Plugin.prototype, {
            init: function () {
                var that = this;
                this.render(),
                    $(window).smartresize(function () {
                        that.render();
                    });
            },
            render: function () {
                this.measure(), "grid" === this.state ? this.doGrid() : this.doList(), this.initialized === !1 && (this.initialized = !0);
            },
            animate: function (data, top, right, width, height, delay) {
                window.setTimeout(function () {
                  
                    data.li.css({ position: "absolute", top: top + "px", right: right + "px", width: width + "px", height: height + "px", opacity: 1 });
                }, delay);
            },
            doList: function () {
                var that = this,
                    top = 0,
                    delay = 0;
                $.each(this.elems, function (current) {
                    current > 0 && (top += that.elems[current - 1].height + that.margin), that.animate(this, top, 0, this.width, this.height, delay), that.initialized === !0 && (delay += that.delay);
                }),
                    window.setTimeout(function () {
                        that.$element.css("height", top + that.elems[that.elems.length - 1].height + "px"), that.toggle.html('<i class="fa fa-th" aria-hidden="true"></i>'), (that.disabled = !1);
                    }, delay);
            },
            doGrid: function () {
                var that = this,
                    width = this.$element.outerWidth(),
                    top = 0,
                    right = 0,
                    delay = this.delay;
                $.each(this.elems, function (current) {
                    if (current > 0) {
                        var newright = right + that.elems[current - 1].width + that.margin;
                        newright + this.width > width ? ((right = 0), (top += that.height + that.margin)) : (right = newright);
                    }
                    that.animate(this, top, right, this.width, that.height, delay), that.initialized === !0 && (delay += that.delay);
                }),
                    window.setTimeout(function () {
                        that.$element.css("height", top + that.height + "px"), that.toggle.html('<i class="fa fa-list" aria-hidden="true"></i>'), (that.disabled = !1);
                    }, delay);
            },
            measure: function () {
                var that = this;
                (this.elems = []), (this.height = 0);
                var parentClass = this.$element.attr("class"),
                    listClass = this.list.attr("class");
                this.list.find("li").each(function () {
                    var parent = $("<div />", { class: parentClass, css: { position: "absolute", top: "0px", right: "-10000px", opacity: 0 } }).appendTo(that.body),
                        ul = $("<ul />", { class: listClass }).appendTo(parent),
                        li = $(this).clone(!1).removeAttr("style");
                    li.appendTo(ul);
                    var width;
                    (width =
                        "grid" === that.state
                            ? li.find(".thumb").outerWidth() +
                            parseInt(li.css("padding-right").replace(/px/, "")) +
                            parseInt(li.css("padding-right").replace(/px/, "")) +
                            parseInt(li.css("border-right-width")) +
                            parseInt(li.css("border-right-width"))
                            : that.$element.outerWidth() -
                            parseInt(that.$element.css("padding-right").replace(/px/, "")) -
                            parseInt(that.$element.css("padding-right").replace(/px/, "")) -
                            parseInt(that.$element.css("border-right-width")) +
                            parseInt(that.$element.css("border-right-width"))),
                        parent.css("width", width + "px");
                    var height = li.outerHeight();
                    that.elems.push({ li: $(this), width: width, height: height }), (that.height = height > that.height ? height : that.height), parent.remove(), (parent = null);
                });
            },
        }),
            ($.fn[pluginName] = function (options) {
                return this.each(function () {
                    $.data(this, "plugin_" + pluginName) || $.data(this, "plugin_" + pluginName, new Plugin(this, options));
                });
            });
    })(jQuery, window, document);
