define([
    'jquery', 'TracingRecordsSettings'
], function ($, tracingRecordsSettings) {
    'use strict';

    function TracingRecordsService() {

        var self = this;
        self.urlLastAdded = tracingRecordsSettings.urlLastAdded;
        self.urlAllRecords = tracingRecordsSettings.urlAllRecords;
        self.traceRecordsContainerId = tracingRecordsSettings.traceRecordsContainerId;
        
        self.updateHistoryOperations = function() {
            return $.getJSON(self.urlLastAdded);
        };

        self.getHistoryOperations = function () {
            return $.getJSON(self.urlAllRecords);
        };
    };

    return new TracingRecordsService();
});