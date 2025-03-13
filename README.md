# JQDatePicker
Date Picker for asp.net webforms

Originates from http://hasheminezhad.com/jqdatepicker which is no longer available!

# Usage

Register the component on Page or User control:

```html
<%@ Register TagPrefix="jq" Namespace="JQControls" Assembly="JQControls" %>
```

Put this in the top of the page or control if you have already jquery registered:

```html
<jq:JQLoader ID="JQLoader2" runat="server" LoadJQScript="False"></jq:JQLoader>

```

If you don't have jquery on the page add this to the top of the page ot control:

```html
<jq:JQLoader ID="JQLoader2" runat="server"></jq:JQLoader>
```

In your form you can use DatePicker as follows:

```html
<jq:JQDatePicker ID="jqDatePicker1" DateFormat="YMD"
   placeholder="..."  CssClass="form-control" runat="server" >
</jq:JQDatePicker>
```



