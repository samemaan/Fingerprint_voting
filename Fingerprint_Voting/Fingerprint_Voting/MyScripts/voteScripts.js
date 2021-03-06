﻿function dragMoveListener(event) {
    var target = event.target,
        // keep the dragged position in the data-x/data-y attributes
        x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
        y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

    // translate the element
    target.style.webkitTransform =
        target.style.transform =
        'translate(' + x + 'px, ' + y + 'px)';

    // update the posiion attributes
    target.setAttribute('data-x', x);
    target.setAttribute('data-y', y);
    //console.log(x)
}

// enable draggables to be dropped into this
interact('.dropzone').dropzone({
    // only accept elements matching this CSS selector
    accept: '#yes-drop',
    // Require a 0% element overlap for a drop to be possible
    overlap: 0,

    ondropactivate: function (event) {
        // add active dropzone feedback
        event.target.classList.add('drop-active');
    },
    ondragenter: function (event) {
        var draggableElement = event.relatedTarget,
            dropzoneElement = event.target;
        // feedback the possibility of a drop
        dropzoneElement.classList.add('drop-target');
        draggableElement.classList.add('can-drop');
        //draggableElement.textContent = 'Dragged in';
        // following line gets the card as it's just changing the colour to active/green shown that users droped the card in the box
        draggableElement.style = '';
        
    },
    ondragleave: function (event) {
        // remove the drop feedback style
        event.target.classList.remove('drop-target');
        event.relatedTarget.classList.remove('can-drop');
        event.relatedTarget.textContent = 'Dragged out';
    },
    ondrop: function (event) {
        //console.log(event)
        var candidate = $('#CandidateId').val();
        //console.log(CandidateId)
        // split the details into an array 
        var canDetails = candidate.split(','); 

        var candidateID = canDetails[0]; 
        var campaignID = canDetails[1];
        // replace any space thats in the id.
        campaignID = campaignID.replace(/\s/g, '');

        console.log(candidateID)
        console.log(campaignID)


        //alert('candidateID: ' + candidateID);

        document.getElementById('CandidateId').value = candidateID; 
        document.getElementById('CampaignID').value = campaignID; 

        //var CampaignId = $('#CampaignID').val();
        //alert('CampaignID' +CampaignId); 

        var theElement = event.relatedTarget,
            theElement = event.target;

        

        // result is just to set to get the endusers confirmation on selected card to redirect them into another page fingerprint varification
        var result = theElement.textContent = 'Vote'
        theElement.style = ""; 

        if (result == 'Vote') {
            var confirmation = confirm("Would you like to vote for the selected candidate?");
            if (confirmation) {
                console.log("When user pressed ok than redirect the user to take his/her fingerprint to validate if he is the user giving vote ");
                // the call for redirecting the user to another page if the user pressed okay.
                values();
            } else {
                // if the user select cancel from the dialog box the current page will be refreshed
                location.reload();
            }

        }
        // Add event handler

        //$("yes-drop").prop('disabled', true);
        //$("dropzone").prop("disabled", true);
        //$("drag-drop").prop("disabled", true);
        //$("dropzone").prop("disabled", true);

        console.log("you are voting right now")


    },
    ondropdeactivate: function (event) {
        // remove active dropzone feedback

        event.target.classList.remove('drop-active');
        event.target.classList.remove('drop-target');
    }
});

interact('.drag-drop')
    .draggable({
        inertia: true,
        restrict: {
            restriction: "parent",
            endOnly: true,
            elementRect: { top: 0, left: 0, bottom: 0, right: 0 }
        },
        autoScroll: true,
        // dragMoveListener from the dragging demo above
        onmove: dragMoveListener,
    });

var UserFingerprint = "";

function values() {
    $("#leForm").submit();
}



// :  
function getImageId(e) {
    var cand = $('#CandidateId').val(e.id);
    //console.log(cand); 
}
