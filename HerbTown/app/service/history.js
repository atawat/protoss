/**
 * Created by Yunjoy on 2015/6/23.
 */
app.service("history", ['$state',function ($state) {
    var _fromStack = [];
    var _nextStack = [];
    var _rootState = "";

    this.goPrev = function () {
        var state = _fromStack[_fromStack.length - 1];
        if (!state)
            state = _rootState;
        $state.go(state);
    };

    this.setPrev = function (fromState, nextState) {
        if(nextState === _rootState){
            _fromStack = [];
            _nextStack = [];
            return;
        }
        var nextIndex = _nextStack.indexOf(fromState);
        var fromIndex = _fromStack.indexOf(nextState);
        if (nextIndex > -1 && fromIndex > -1) {
            for (var i = 0; i < (_fromStack.length - fromIndex); i++) {
                _fromStack.pop();
                _nextStack.pop();
            }
            return;
        }
        if (fromState)
            _fromStack.push(fromState);
        if (nextState)
            _nextStack.push(nextState);

    };

    this.init = function (rootState) {
        if (rootState)
            _rootState = rootState;
    }
}]);