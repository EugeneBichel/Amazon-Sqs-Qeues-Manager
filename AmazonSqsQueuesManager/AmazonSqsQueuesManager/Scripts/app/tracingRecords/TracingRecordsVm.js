define(['ko', 'extensions'], function (ko) {
    'use strict';

    function TracingHistory(items) {

        var self = this;
        self.historyItems = ko.observableArray(items);

        self.addItem = function (item) {
            var newItem = ko.observable(item);
            self.historyItems.setAtTop(newItem);
        };

        self.addItems = function (items) {
            self.historyItems = ko.observableArray(items);
        };
    };

    return new TracingHistory([]);
});