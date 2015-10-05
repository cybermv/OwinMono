(function (angular) {
    'use strict';

    var fruitsModule = angular.module('fruits', []);

    fruitsModule.service('FruitsService', [
        '$http', '$q',
        function ($http, $q) {
            var service = this;

            function handleSuccess(response) {
                return $q.resolve(response.data);
            }

            function handleError(response) {
                return $q.reject(response);
            }

            service.getAll = function () {
                return $http.get('/api/fruits').then(handleSuccess, handleError);
            };

            service.getById = function (id) {
                return $http.get('/api/fruits/' + id).then(handleSuccess, handleError);
            };

            service.getColors = function () {
                return $http.get('/api/fruits/colors').then(handleSuccess, handleError);
            };

            service.create = function (newFruit) {
                return $http.put('/api/fruits', newFruit).then(handleSuccess, handleError);
            };

            service.update = function (id, fruit) {
                return $http.patch('/api/fruits/' + id, fruit).then(handleSuccess, handleError);
            };

            service.delete = function (id) {
                return $http.delete('/api/fruits/' + id).then(handleSuccess, handleError);
            };
        }
    ]);

    fruitsModule.filter('KeyToValue', function () {
        return function (key, keyValueArray) {
            var toReturn = key;
            angular.forEach(keyValueArray, function (item) {
                if (item.key === key) {
                    toReturn = item.value;
                }
            });
            return toReturn;
        };
    });

    fruitsModule.directive('fruitManager', function () {
        return {
            restrict: 'E',
            templateUrl: 'templates/fruitManager.html',
            controller: ['$scope', 'FruitsService', function ($scope, fruitsService) {
                $scope.fruitsService = fruitsService;

                $scope.handleError = function (response) {
                    var errorMessage = response.status + ' - ' +
                    response.statusText + '; ' +
                    (response.data.message || 'unknown error');

                    alert(errorMessage);
                }

                fruitsService.getColors().then(
                    function (data) {
                        $scope.fruitColors = data;
                    },
                    function (error) {
                        $scope.handleError(error);
                    });

                fruitsService.getAll().then(
                    function (data) {
                        $scope.fruits = data;
                    },
                    function (error) {
                        $scope.handleError(error);
                    });
            }],
            link: function (scope, elem, attrs, ctrl) {
                scope.createFruit = function () {
                    scope.fruits.push({
                        id: -1,
                        name: 'New fruit',
                        color: scope.fruitColors[0].key,
                        price: 0,
                        editMode: true
                    });
                }

                scope.editFruit = function (fruit) {
                    fruit._original = angular.copy(fruit);
                    fruit.editMode = true;
                };

                scope.undoEditFruit = function (fruit) {
                    if (fruit.id > 0) {
                        var original = fruit._original;
                        angular.extend(fruit, original);
                        delete fruit._original;
                        fruit.editMode = false;
                    } else {
                        var index = scope.fruits.indexOf(fruit);
                        scope.fruits.splice(index, 1);
                    }
                };

                scope.deleteFruit = function (id) {
                    scope.fruitsService.delete(id).then(
                        function (data) {
                            angular.forEach(scope.fruits, function (fruit, idx) {
                                if (fruit.id === id) {
                                    scope.fruits.splice(idx, 1);
                                }
                            });
                        },
                        function (error) {
                            scope.handleError(error);
                        });
                };

                scope.saveFruit = function (fruit) {
                    if (fruit.id > 0) {
                        scope.fruitsService.update(fruit.id, fruit).then(
                            function (data) {
                                fruit.editMode = false;
                            },
                            function (error) {
                                scope.handleError(error);
                            });
                    } else {
                        scope.fruitsService.create(fruit).then(
                            function (data) {
                                fruit.id = data;
                                fruit.editMode = false;
                            },
                            function (error) {
                                scope.handleError(error);
                            });
                    }
                };
            }
        }
    });
})(window.angular);