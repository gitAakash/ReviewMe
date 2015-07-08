var isEditable = false;

jQuery(document).ready(function () {
    $(".input-21f").rating({
        starCaptions: function (val) {

            if (val < 3) {
                return val;
            } else {
                return 'high';
            }
        },
        starCaptionClasses: function (val) {
            if (val < 3) {
                return 'label label-danger';
            } else {
                return 'label label-success';
            }
        },
        hoverOnClear: false
    });

    $('#rating-input').rating({
        min: 0,
        max: 5,
        step: 1,
        size: 'lg',
        showClear: false
    });
});

//This function is calling from star-rating.js
function SetValueToHiddenField(Name, Value) {
  
}