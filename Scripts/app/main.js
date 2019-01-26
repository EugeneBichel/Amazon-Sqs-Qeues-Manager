require.config({
    paths: {
        'jquery': '../jquery-1.10.2',
        'ko': '../knockout-3.3.0',
        'extensions': 'extensions',
        'TracingRecordsVm': 'tracingRecords/TracingRecordsVm',
        'TracingRecordsService': 'tracingRecords/TracingRecordsService',
        'TracingRecordsSettings': 'tracingRecords/TracingRecordsSettings',
        'QueueMessagesVm': 'queueMessages/QueueMessagesVm',
        'QueueMessagesService': 'queueMessages/QueueMessagesService',
        'QueueMessagesSettings': 'queueMessages/QueueMessagesSettings',
        'ErrorMessageVm': 'errorMessages/ErrorMessageVm',
        'ErrorMessageSettings': 'errorMessages/ErrorMessageSettings',
        'InfoMessageVm': 'infoMessages/InfoMessageVm',
        'InfoMessageSettings': 'infoMessages/InfoMessageSettings'
    }
});

    require(
        [
            'jquery',
            'ko',
            'TracingRecordsVm',
            'TracingRecordsService',
            'TracingRecordsSettings',
            'QueueMessagesVm',
            'QueueMessagesService',
            'QueueMessagesSettings',
            'ErrorMessageVm',
            'ErrorMessageSettings',
            'InfoMessageVm',
            'InfoMessageSettings'
        ],
        function($,
            ko,
            tracingRecordsVm,
            tracingRecordsService,
            tracingRecordsSettings,
            queueMessagesVm,
            queueMessagesService,
            queueMessagesSettings,
            errorMessageVm,
            errorMessageSettings,
            infoMessageVm,
            infoMessageSettings) {
            'use strict';

            $(function() {
                tracingRecordsService.getHistoryOperations()
                    .done(function(returnedData) {
                        tracingRecordsVm.addItems(returnedData);
                        ko.applyBindings(tracingRecordsVm, document.getElementById(tracingRecordsSettings.containerId));
                    });

                queueMessagesSettings.btnGetCount.click(function() {
                    getMessagesCount();
                });
                queueMessagesSettings.btnDequeue.click(function () {

                    setDisabled(queueMessagesSettings.btnDequeue);
                    infoMessageVm.message('Please wait...');

                    queueMessagesService.getMessagesCount()
                        .done(function (returnedData) {
                            var count = JSON.parse(returnedData);
                            queueMessagesVm.messagesCount(count);
                            errorMessageVm.message('');

                            if (count == 0) {
                                infoMessageVm.message('Queue is empty. Add item to queue before run "dequeue" action.');
                                queueMessagesVm.messageFromQueue('');
                            } else {
                                infoMessageVm.message('');
                                dequeueMessage();
                            }
                        })
                        .fail(function (jqxhr, textStatus, error) {
                            if (jqxhr.status === 200) {
                                errorMessageVm.message('');
                                queueMessagesVm.messageToQueue('');
                            } else {
                                updateErrorMessage(textStatus, error);
                            }
                            infoMessageVm.message('');
                        })
                        .always(function () {
                            setEnabled(queueMessagesSettings.btnDequeue);
                        });
                });
                queueMessagesSettings.btnEnqueue.click(function() {
                    setDisabled(queueMessagesSettings.btnEnqueue);
                    infoMessageVm.message('Please wait...');

                    var message = { Body: queueMessagesVm.messageToQueue() };
                    var json = JSON.stringify(message);

                    queueMessagesService.engueueMessage(json)
                        .fail(function (jqxhr, textStatus, error) {
                            if (jqxhr.status === 200) {
                                errorMessageVm.message('');
                                queueMessagesVm.messageToQueue('');
                            }
                            else {
                                updateErrorMessage(textStatus, error);
                            }
                        })
                        .always(function () {
                            updateHistoryOperations();
                            setEnabled(queueMessagesSettings.btnEnqueue);
                            infoMessageVm.message('');
                        });
                });
                queueMessagesSettings.btnPurgeQueue.click(function() {
                    setDisabled(queueMessagesSettings.btnPurgeQueue);
                    infoMessageVm.message('Please wait...');

                    queueMessagesService.purgeQueue()
                        .done(function () {
                            queueMessagesVm.messageFromQueue('');
                            queueMessagesVm.messagesCount(0);
                            infoMessageVm.message('Queue is empty');
                        })
                        .fail(function(jqxhr, textStatus, error) {
                            if (jqxhr.status === 200) {
                                errorMessageVm.message('');
                                queueMessagesVm.messageFromQueue('');
                                queueMessagesVm.messagesCount(0);
                                infoMessageVm.message('Queue is empty');
                            }
                            else {
                                updateErrorMessage(textStatus, error);
                                infoMessageVm.message('');
                            }
                        })
                        .always(function() {
                            updateHistoryOperations();
                            setEnabled(queueMessagesSettings.btnPurgeQueue);
                        });
                });

                function updateHistoryOperations() {
                    tracingRecordsService.updateHistoryOperations()
                        .done(function (returnedData) {
                            tracingRecordsVm.addItem(returnedData);
                        })
                        .fail(function (jqxhr, textStatus, error) {
                            if (jqxhr.status === 200) {
                                errorMessageVm.message('');
                                queueMessagesVm.messageToQueue('');
                            } else {
                                updateErrorMessage(textStatus, error);
                            }
                    });
                };

                function dequeueMessage() {
                    infoMessageVm.message('Please wait...');

                    queueMessagesService.dequeueMessage()
                        .done(function (returnedData) {
                            var message = returnedData;
                            queueMessagesVm.messageFromQueue(message);
                            errorMessageVm.message('');
                        })
                        .fail(function (jqxhr, textStatus, error) {
                            if (jqxhr.status === 200) {
                                errorMessageVm.message('');
                                queueMessagesVm.messageToQueue('');
                            } else {
                                updateErrorMessage(textStatus, error);
                            }
                        })
                        .always(function () {
                            updateHistoryOperations();
                            setEnabled(queueMessagesSettings.btnDequeue);
                            infoMessageVm.message('');
                        });
                };

                function getMessagesCount() {
                    setDisabled(queueMessagesSettings.btnGetCount);
                    infoMessageVm.message('Please wait...');

                    queueMessagesService.getMessagesCount()
                        .done(function (returnedData) {
                            var count = JSON.parse(returnedData);
                            queueMessagesVm.messagesCount(count);
                            errorMessageVm.message('');
                        })
                        .fail(function (jqxhr, textStatus, error) {
                            if (jqxhr.status === 200) {
                                errorMessageVm.message('');
                                queueMessagesVm.messageToQueue('');
                            } else {
                                updateErrorMessage(textStatus, error);
                            }
                        })
                        .always(function () {
                            updateHistoryOperations();
                            setEnabled(queueMessagesSettings.btnGetCount);
                            infoMessageVm.message('');
                        });
                };

                ko.applyBindings(errorMessageVm, document.getElementById(errorMessageSettings.containerId));
                ko.applyBindings(infoMessageVm, document.getElementById(infoMessageSettings.containerId));
                ko.applyBindings(queueMessagesVm, document.getElementById(queueMessagesSettings.containerId));
                

                function setEnabled(elem) {
                    elem.prop('disabled', false);
                };
                function setDisabled(elem) {
                    elem.prop('disabled', true);
                };
                function updateErrorMessage(textStatus, error) {
                    var err = textStatus + ", " + error;
                    errorMessageVm.message("Request failed: " + err);
                };
            });
        });