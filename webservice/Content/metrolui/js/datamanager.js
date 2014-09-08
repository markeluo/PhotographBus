var datamanager = {
    url: "http://localhost:57504/service.asmx",
    ReadData:function (_obj) {
        var posturl = this.url + "/" + _obj.MethodName;
        $.ajax({
            type: "POST",
            url: posturl,
            dataType: "jsonp",
            data: { "jsonData": _obj.Paras },
            success: function (_result) {
                 _obj.CallBackFunction(JSON.parse(_result));
            },
            error: function (_result) {
                _obj.CallBackFunction(JSON.parse(_result));
            }
        });
    },
    ReadHTML:function (_obj) {
        var posturl = this.url + "/" + _obj.MethodName;
        $.ajax({
            type: "POST",
            url: posturl,
            dataType: "jsonp",
            data: { "jsonData": _obj.Paras },
            success: function (_result) {
                _obj.CallBackFunction(_result);
            },
            error: function (_result) {
                _obj.CallBackFunction(_result);
            }
        });
    }
}