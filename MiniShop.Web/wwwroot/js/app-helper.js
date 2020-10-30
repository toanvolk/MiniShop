var helper = {
    createGUID: function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    },
    colorRandom: function () {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    },
    showDialog: function (obj) {
        //obj{ contentData {url, type, data}, config}      
        let _contentData = {
            url: "",
            type: "POST",
            data: {},            
        }

        if (obj.contentData) {
            $.each(obj.contentData, function (key, value) {
                _contentData[key] = value;
            });
        }

        let _config = {
            name: "k-window-custom-" + helper.createGUID(),
            title: "Title-window",
            draggable: true,
            resizable: false,
            width: "600px",
            modal: true,
            actions: [
                "Maximize",
                "Close"
            ],
            activate: function (e) {
                this.center();
            },
            close: function (e) {
                e.sender.element.data('handler').destroy();
            },
            visible: true
        }
        if (obj.config) {
            $.each(obj.config, function (key, value) {
                if (key == 'close') {
                    _config[key] = function (e) {
                        value(e);
                        e.sender.element.data('handler').destroy();
                    }
                }
                else if (key == 'activate') {
                    _config[key] = function (e) {
                        value(e);
                        e.sender.center();
                    }
                }
                else {
                    _config[key] = value;
                }
            });
        }

        $.ajax({
            url: _contentData.url,
            type: _contentData.type,
            data: _contentData.data
        }).done(function (res) {
            $(res)
                .kendoWindow(_config);
        })
    },
    closeDialog: function (content, callback) {
        if (callback) callback(content);
        content.data("handler").close();
    },
    inputValidate: {
        checkRequired: function (obj) {
            const VALIDATE_MESSAGE_REQUIRED = 'validate-message-required';
            let _valid = true;
            let _html = '<div class="invalid-feedback">{{message}}</div>';
            let _data = {
                content: undefined,
                takeIgnore: false //FALSE: KHÔNG duyệt dom có thuộc tính ignore. TRUE: duyệt dom có thuộc tính ignore.
            };
            if (obj) {
                //vùng được check input
                _data.content = obj.content || _data.content;
                //Cho phép check input có property IGNORE. Mặc định check = true
                _data.takeIgnore = obj.takeIgnore || _data.takeIgnore;
            }
            let _content;
            if (typeof (_data.content) == 'object') {
                if (!_data.takeIgnore)
                    _content = _data.content.find('input[required]:not([ignore])');
                else
                    _content = _data.content.find('input[required]');
            }
            else {
                _content = (_data.content ? _data.content + " " : "") + "input[required]";
                if (!_data.takeIgnore)
                    _content = _content + ':not([ignore])';
                _content = $(_content);
            }

            _content.each(function (i, dom) {
                if (!$(dom).val()) {
                    if (!$(dom).next().hasClass('invalid-feedback')) {
                        let _message = $(dom).attr(VALIDATE_MESSAGE_REQUIRED);
                        let _htmlAppend = _html;
                        _message = _message || "Please enter..!";
                        let _regex = new RegExp("{{message}}", "gi");
                        _htmlAppend = _htmlAppend.replace(_regex, _message)
                        $(_htmlAppend).insertAfter($(dom));
                    }
                    $(dom).next().show();
                    _valid = false;
                    return;

                }
                else {
                    if ($(dom).next().hasClass('invalid-feedback')) {
                        $(dom).next().hide();
                    }
                }
            });
            return _valid;
        }
    },
    formData: {
        inputToObject: function (content, callback) {
            let _obj = {};

            let _dataArray = $(content).find(":input").not('input[ignore]').serializeArray();
            _dataArray.forEach(element => {
                _obj[element.name] = element.value
            });

            if (typeof (callback) == "function") callback(_obj);
            return _obj;
        },
        objectToInput: function (object, content, specifyMap, callback) {
            if (typeof (specifyMap) == "undefined") {
                $.each(object, function (key, value) {
                    content.find("Input[name=" + key + "]")?.val(value);
                });
            }
            else {
                if (Array.isArray(specifyMap)) {
                    specifyMap.forEach(element => {
                        content.find("input[name=" + element.inputName + "]")?.val(object[element.propertyName])
                    });
                }
            }
            if (typeof (callback) == "function") callback(object, content);
        },
        clearInput: function (obj) {
            let _data = {
                content: "",
                fieldExpel: [],
                callback: undefined
            };
            if (obj) {
                $.each(obj, function (key, value) {
                    _data[key] = value;
                });
            }

            let _inputString = 'input', _ignoreField = '';//$(temp1).find('input:not([name=Type], [name=Name])')
            $.each(_data.fieldExpel, function (index, value) {
                _ignoreField += '[name=' + value + '],';
            });
            if (_ignoreField) {
                _ignoreField = _ignoreField.substr(0, _ignoreField.length - 1);
                _inputString += ':not(' + _ignoreField + ')';
            }

            if (typeof (_data.content) == 'object') {
                _data.content.find(_inputString).val('');
            }
            else {
                $(_data.content).find(_inputString).val('');
            }
            if (_data.callback) _data.callback(_data.content);
        }
    },
    formatNumber: {
        addCommas: function (nStr) {
            nStr += '';
            var x = nStr.split('.');
            var x1 = x[0];
            var x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }
    },
    file: {
        fileDialog: function (content, objConfig, callbackAfterChoose) {
            let _newId = helper.createGUID(); 
            let _html = `<div id="file-manager-{id}">
                            <div id="file-manager-content-{id}"></div>
                            <div style="align-content: revert;margin-top: 10px;">
                                <button class="btn btn-sm btn-primary float-right btn-choose">Choose</button>
                            </div>
                        </div>`;
            _html = _html.replaceAll(new RegExp("{id}", 'gi'), _newId);
            $(_html).appendTo(content);
            let _contentFileManage = content.find('#file-manager-content-' + _newId).last();
            let _contentShowDialog = content.find('#file-manager-' + _newId).last();

            //register event choose
            $('#file-manager-' + _newId + ' .btn-choose').off('click').on('click', function (e) {
                let _selects = $('#file-manager-content-' + _newId).data('kendoFileManager').getSelected();
                callbackAfterChoose(_selects);
                helper.closeDialog(_contentShowDialog);
            });


           

            let _configKendoWindow = {
                name: "k-window-custom-" + _newId,
                title: "Title-window",
                draggable: true,
                resizable: false,
                width: "850px",
                modal: true,
                actions: [
                    "Maximize",
                    "Close"
                ],
                activate: function (e) {
                    this.center();
                },
                close: function (e) {
                    e.sender.element.data('handler').destroy();                  
                },
                visible: true
            }
            //Map obj
            if (objConfig) {
                $.each(objConfig, function (key, value) {
                    _configKendoWindow[key] = value;
                });
            }

            kendo.syncReady(function () {
                _contentFileManage.kendoFileManager({
                    "toolbar": {
                        "items": [{
                            "name": "createFolder"
                        }, {
                            "name": "upload"
                        }, {
                            "name": "sortDirection"
                        }, {
                            "name": "sortField"
                        }, {
                            "name": "changeView"
                        }, {
                            "name": "spacer"
                        }, {
                            "name": "details"
                        }, {
                            "name": "search"
                        }
                        ]
                    },
                    "contextMenu": {
                        "items": [{
                            "name": "rename"
                        }, {
                            "name": "delete"
                        }
                        ]
                    },
                    "dataSource": {
                        "transport": {
                            "read": {
                                "url": "/admin/FileManagerData/Read",
                                "type": "POST"
                            },
                            "update": {
                                "url": "/admin/FileManagerData/Update",
                                "type": "POST"
                            },
                            "create": {
                                "url": "/admin/FileManagerData/Create",
                                "type": "POST"
                            },
                            "destroy": {
                                "url": "/admin/FileManagerData/Destroy",
                                "type": "POST"
                            }
                        }
                    },
                    "uploadUrl": "/admin/FileManagerData/Upload"
                });
                _contentShowDialog.kendoWindow(_configKendoWindow);
            });
                        
        }
    },
    editor: {
        init: function (content) {
            content.kendoEditor({
                stylesheets: [
                    "../content/shared/styles/editor.css",
                ],
                tools: [
                    "bold",
                    "italic",
                    "underline",
                    "justifyLeft",
                    "justifyCenter",
                    "justifyRight",
                    "insertUnorderedList",
                    "createLink",
                    "unlink",
                    "insertImage",
                    "tableWizard",
                    "createTable",
                    "addRowAbove",
                    "addRowBelow",
                    "addColumnLeft",
                    "addColumnRight",
                    "deleteRow",
                    "deleteColumn",
                    "mergeCellsHorizontally",
                    "mergeCellsVertically",
                    "splitCellHorizontally",
                    "splitCellVertically",
                    "formatting",
                    {
                        name: "fontName",
                        items: [
                            { text: "Andale Mono", value: "Andale Mono" },
                            { text: "Arial", value: "Arial" },
                            { text: "Arial Black", value: "Arial Black" },
                            { text: "Book Antiqua", value: "Book Antiqua" },
                            { text: "Comic Sans MS", value: "Comic Sans MS" },
                            { text: "Courier New", value: "Courier New" },
                            { text: "Georgia", value: "Georgia" },
                            { text: "Helvetica", value: "Helvetica" },
                            { text: "Impact", value: "Impact" },
                            { text: "Symbol", value: "Symbol" },
                            { text: "Tahoma", value: "Tahoma" },
                            { text: "Terminal", value: "Terminal" },
                            { text: "Times New Roman", value: "Times New Roman" },
                            { text: "Trebuchet MS", value: "Trebuchet MS" },
                            { text: "Verdana", value: "Verdana" },
                        ]
                    },
                    "fontSize",
                    "foreColor",
                    "backColor",
                    "insertImage",
                    "insertFile"
                ],
                imageBrowser: {
                    messages: {
                        dropFilesHere: "Drop files here"
                    },
                    transport: {
                        read: "/kendo-ui/service/ImageBrowser/Read",
                        destroy: {
                            url: "/kendo-ui/service/ImageBrowser/Destroy",
                            type: "POST"
                        },
                        create: {
                            url: "/kendo-ui/service/ImageBrowser/Create",
                            type: "POST"
                        },
                        thumbnailUrl: "/kendo-ui/service/ImageBrowser/Thumbnail",
                        uploadUrl: "/kendo-ui/service/ImageBrowser/Upload",
                        imageUrl: "/kendo-ui/service/ImageBrowser/Image?path={0}"
                    }
                },
                fileBrowser: {
                    messages: {
                        dropFilesHere: "Drop files here"
                    },
                    transport: {
                        read: "/kendo-ui/service/FileBrowser/Read",
                        destroy: {
                            url: "/kendo-ui/service/FileBrowser/Destroy",
                            type: "POST"
                        },
                        create: {
                            url: "/kendo-ui/service/FileBrowser/Create",
                            type: "POST"
                        },
                        uploadUrl: "/kendo-ui/service/FileBrowser/Upload",
                        fileUrl: "/kendo-ui/service/FileBrowser/File?fileName={0}"
                    }
                }
            });
        }
    }
}