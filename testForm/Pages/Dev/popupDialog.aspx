<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popupDialog.aspx.cs" Inherits="testForm.Pages.Dev.popupDialog" %>

<!DOCTYPE html>

<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>jQuery UI Dialog - Modal confirmation</title>
  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
  $( function() {
    $( "#dialog-confirm" ).dialog({
      resizable: false,
      height: "auto",
      width: 400,
      modal: true,
      buttons: {
        "Yes": function() {
          $( this ).dialog( "close" );
        },
        "No": function() {
          $( this ).dialog( "close" );
        }
      }
    });
  } );
  </script>
</head>
<body>
 
<div id="dialog-confirm" title="Empty the recycle bin?">
  <p><span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>These items will be permanently deleted and cannot be recovered. Are you sure?</p>
</div>
 
 
</body>
</html>