function dragMoveListener(event) {
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
    // Require a 75% element overlap for a drop to be possible
    overlap: 0.75,

    // listen for drop related events:

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
        draggableElement.textContent = 'Dragged in';



    },
    ondragleave: function (event) {
        // remove the drop feedback style
        event.target.classList.remove('drop-target');
        event.relatedTarget.classList.remove('can-drop');
        event.relatedTarget.textContent = 'Dragged out';
        // reload the page when the user draged out the candidate card from the droped zone
        //location.reload();
    },
    ondrop: function (event) {
        //console.log(event)

        var result = event.relatedTarget.textContent = "Vote"; 
      


        if (result == 'Vote') {
            
            var confirmation = confirm("Would you like to vote for the selected candidate?");
            if (confirmation) {
                console.log("When user pressed ok than redirect the user to take his/her fingerprint to validate if he is the user giving vote ");
                //window.location.replace('https://flaviocopes.com/how-to-redirect-using-javascript/')
                //window.location.replace('@Html.ActionLink("Vote", "Index", "Vote")')
                // call the funciton to get the candidateId and userId, than passs
                values();
            } else {
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
    var CandidateId = document.getElementById('CandidateId').value;
    var UserId = document.getElementById('UserId').value;
    UserFingerprint = document.getElementById('UserFingerprint').value;
    //var b = document.getElementById('b').value;
    // printing the user details with candidate for testing
     
    console.log(CandidateId);
    console.log(UserId);
    console.log(UserFingerprint);
    $("#leForm").submit();
    //window.stop();
}


function fingerprint() {


    UserFingerprint = document.getElementById('UserFingerprint').value;

}