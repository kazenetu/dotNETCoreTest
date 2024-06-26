var myApp = angular.module('webApiService', []);
myApp.service('webApiService',['$resource', function($resource) {

  this.baseUri = "api/";

  /**
   * 基本URIを設定する
   */
  this.setBaseUri = function(uri){
    this.baseUri = uri;
  }

    /**
     * GET(配列取得)メソッド
     */
    this.query = function(action,params,callFunction){
      var result = $resource(this.baseUri + action).query(getParams(params));

      result.$promise.then(function (response) {
          callFunction(response);
      }, function (response) {
        window.location = "/";
      });
    };

    /**
     * GETメソッド
     */
    this.get = function(action,params,callFunction){
      var result = $resource(this.baseUri + action).get(getParams(params));

      result.$promise.then(function (response) {
          callFunction(response);
      }, function (response) {
        window.location = "/";
      });
    };

    /**
     * POSTメソッド:Upload
     */
    this.postUpload = function(action,params,callFunction){
      var result = $resource(this.baseUri + action,null,
        {
          'save':{
            method:'POST',
            transformRequest: null,
            headers: {'Content-type':undefined}
          }
        }
      ).save(params);

      result.$promise.then(function (response) {
          callFunction(response);
      }, function (response) {
          window.location = "/";
      });
    };

    /**
     * POSTメソッド
     */
    this.post = function(action,params,callFunction){
      var result = $resource(this.baseUri + action).save(getParams(params));

      result.$promise.then(function (response) {
          callFunction(response);
      }, function (response) {
          window.location = "/";
      });
    };

    /**
     * POST(配列取得)メソッド
     */
    this.postQuery = function(action,params,callFunction){
        var result = $resource(this.baseUri + action,null,{query: {method: 'post', isArray: true}}).query(getParams(params));

      result.$promise.then(function (response) {
          callFunction(response);
      }, function (response) {
        window.location = "/";
      });
    };

    /**
     * 整備済の送信パラメータ取得
     */
    function getParams(srcParam){
      var result = {};

      var keys = Object.keys(srcParam);
      for(var keyIndex=0;keyIndex<keys.length;keyIndex++){
          var key = keys[keyIndex];
          var value = srcParam[key];
          var type = 'none';

          type = Object.prototype.toString.call(value).slice(8, -1).toLowerCase();

          if(type === 'object'){
              value = getParams(value);
          }else{
              if(type === 'string'){
                  // 後ろスペース除去
                  value = value.replace(/[\s]+$/g,'')
              }
          }
          result[key] = value;
      }
      return result;
  }

}]);
 