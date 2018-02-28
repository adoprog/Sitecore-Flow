(function (speak) {
  var parentApp = window.parent.Sitecore.Speak.app.findApplication('EditActionSubAppRenderer');
  speak.pageCode([], function () {
    return {
      initialized: function () {
        this.on({
          "loaded": this.loadDone
        }, this);

        if (parentApp) {
          parentApp.loadDone(this, this.HeaderTitle.Text, this.HeaderSubtitle.Text);
          debugger;
          parentApp.setSelectability(this, true, null);
        }
      },

      loadDone: function (parameters) {
        this.Parameters = parameters || {};
        this.TriggerAddress.Value = this.Parameters.triggerAddress;
      },

      getData: function () {
        this.Parameters.triggerAddress = this.TriggerAddress.Value;
        return this.Parameters;
      }
    };
  });
})(Sitecore.Speak);