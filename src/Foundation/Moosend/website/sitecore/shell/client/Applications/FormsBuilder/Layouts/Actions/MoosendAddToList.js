(function (speak) {
    var parentApp = window.parent.Sitecore.Speak.app.findApplication('EditActionSubAppRenderer');

    var fields = ["Email", "Name", "Tags"];

    speak.pageCode(["underscore"],
        function (_) {
            return {
                initialized: function () {
                    this.on({
                            "loaded": this.loadDone
                        },
                        this);

                    this.dataLoaded = false;

                    this.MessageInfoForm.List.on("change:SelectedValue", this.changeList, this);
                    this.CurrentFields = [];

                    if (parentApp) {
                        parentApp.loadDone(this, this.HeaderTitle.Text, this.HeaderSubtitle.Text);
                    }
                },

                changeList: function () {
                    var listName = this.MessageInfoForm.List.SelectedValue;
                    parentApp.setSelectability(this, !!listName);
                    var item = this.MessageInfoForm.List.SelectedItem;
                    if (!item || !this.$Mapping) {
                        return;
                    }
                    var moosendProperties = item.List.CustomProperties;
                    var currentFields = fields.slice();
                    for (var i = 0; i < moosendProperties.length; i++) {
                        currentFields.push(moosendProperties[i].Name);
                    }
                    this.CurrentFields = currentFields;
                    
                    var formFields = this.FormClientApi.getFields();
                    
                    var $sitecoreFormFieldsSelector = $(`<select class="form-control sc-droplist" style="margin: 5px;">
<option value=""></option>
</select>`);
                    for (var i = 0; i < formFields.length; i++){
                        $sitecoreFormFieldsSelector.append(`<option value="${formFields[i].itemId}">${formFields[i].name}</option>`);
                    }

                    this.$Mapping.children('[data-fieldname]').hide();
                    
                    for (var i = 0; i < currentFields.length; i++) {
                        var currentField = currentFields[i];
                        var $existing = this.$Mapping.find(`[data-fieldname='${currentFields[i]}']`);
                        if ($existing.length){
                            $existing.show();
                            continue;
                        }
                        var $mapping = $(`
   <div data-fieldname="${currentField}">
      <div title="${currentField}:">
         <div class="sc-formlabeltop visible-xs">
            <div class="sc-global-isrequired"> 
            </div>
            <span>${currentField}:</span>
         </div>
         <div class="sc-formlabelleft hidden-xs">
            <span>${currentField}:</span>
         </div>
      </div>
      <div class="sc-formcomponent">
         <div class="sc-global-isrequired"></div>
         <div class="sc-formcomponent-wrapper">
            ${$sitecoreFormFieldsSelector[0].outerHTML}
            <div class="sc-formhelptext" title=""><span></span></div>
         </div>
      </div>
   </div>
`);
                        this.$Mapping.append($mapping);
                        (function($mapping, currentField, parameters) {
                            var initialValue = parameters.mapping && parameters.mapping[currentField] || '';
                            $mapping.find('select').val(initialValue);
                            $mapping.find('select').on('change', function (elem) {
                                var value = $(this).val();
                                console.log(parameters, currentField, value);
                                parameters.mapping[currentField] = value;
                            });
                        })($mapping, currentField, this.Parameters);
                    }
                    console.log('currentFields', currentFields)
                },

                loadDone: function (parameters) {
                    if (this.dataLoaded) {
                        return;
                    }
                    this.dataLoaded = true;
                    this.Parameters = parameters || {};
                    if (!this.Parameters.mapping){
                        this.Parameters.mapping = {};
                    }
                    this.MessageInfoForm.BindingTarget = this.Parameters;
                    $.ajax({
                        url: "/sitecore/api/ssc/Moosend/MoosendServiceApi/Moosend/All",
                        type: "GET",
                        dataType: "json",
                        context: this,
                        success: function (data) {
                            var items = $.map(data.Data, function (x) {
                                return {
                                    Value: x.Id,
                                    Name: x.Name,
                                    List: x,
                                }
                            });
                            this.MessageInfoForm.List.Items = items;
                            this.MessageInfoForm.List.trigger('itemsChanged', items);
                            this.MessageInfoForm.trigger("initializedProperties", this);
                            this.MessageInfoForm.IsVisible = true;
                            this.renderMappingPart();
                            this.MessageInfoForm.List.trigger("change:SelectedValue", this.Parameters.list);
                            console.log(this.FormClientApi.getFields())
                        }
                    })
                },

                getData: function () {
                    this.MessageInfoForm.trigger("formSubmit", this);
                    var formData = this.MessageInfoForm.getFormData(),
                        keys = _.keys(formData);

                    keys.forEach(function (propKey) {
                        if (formData[propKey] === null || formData[propKey].length === 0) {
                            if (this.Parameters.hasOwnProperty(propKey)) {
                                delete this.Parameters[propKey];
                            }
                        } else {
                            this.Parameters[propKey] = formData[propKey];
                        }
                    }.bind(this));
                    var mappingKeys = Object.keys(this.Parameters.mapping);
                    for(var i = 0; i < mappingKeys.length; i++){
                        if (this.CurrentFields.indexOf(mappingKeys[i]) < 0){
                            delete this.Parameters.mapping[mappingKeys[i]];
                        }
                    }
                    return this.Parameters;
                },

                getDescription: function () {
                    return this.MessageInfoForm.List.SelectedItem.Name;
                },

                renderMappingPart: function () {
                    this.$Mapping = $('<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12  sc-bottom-padding">' +
                        '<p style="margin: 4px 0 12px;">Sitecore Send fields mapping:</p>' +
                        '</div>');
                    $(this.MessageInfoForm.FormRoot.el).append(this.$Mapping)
                },
            };
        });
})(Sitecore.Speak);