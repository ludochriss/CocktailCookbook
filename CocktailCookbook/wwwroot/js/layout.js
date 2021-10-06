const sidebar = document.getElementById('sidebar');
const toggle = document.getElementById('navToggle');

const content = document.getElementById('content');

let assign = document.getElementById('assign');
let remove = document.getElementById('remove');
let assignDiv = document.getElementById('assignmentContainer');
let removeDiv = document.getElementById('removalContainer');
let open = document.getElementById('openRoles');
let openRemove = document.getElementById('openRoleRemoval');
let toggleRole = document.getElementById('toggleRoles');

//toggles the sidebar
toggle.onclick = () => {
    
    if (!sidebar.classList.contains('active')) {
        console.log('Doesnt contain active');
        content.style.marginLeft = '180px';
    }
    else {
        content.style.marginLeft = '0px';
    }
    sidebar.classList.toggle('active');
    
      
}


//show/hide the role assignment and removal div's
assign.onclick = () => {
   
    if (assignDiv.style.display == '') {
        assignDiv.style.display = 'none';
    }
    

    if (assignDiv.style.display.toString() == "block" || assignDiv.style.display.toString() == 'none') {

        if (assignDiv.style.display == 'none') {
            assignDiv.style.display = 'block';
        }
        else {
            assignDiv.style.display = 'none';
        }
    }

}

remove.onclick = () => {
   
    if (removeDiv.style.display == '') {
        removeDiv.style.display = 'none';
    }
    if (removeDiv.style.display.toString() == 'none') {
        removeDiv.style.display = 'block';
    }
    else {
        removeDiv.style.display = 'none';
    }

}
openRemove.onclick = () => {
    debugger;
    document.getElementById('userRoleRemovalContainer').style.display = 'block';
   
}
open.onclick = () => {
    
    document.getElementById('userRoleAssignContainer').style.display = 'block';
}

  

        // Add the following code if you want the name of the file appear on select

    

//used User.Name as the id for two Span elements that would be hidden based on user choices
//quick fix by using user Id in the removal process.
//Potentially have one form that changes button information based on user input.
function Transmit(name, id,  type) {
   
    if (type == 'assign') {
        debugger;
        open.innerText = 'Assign Roles for ' + name;
        open.classList = 'btn btn-warning';
               
        document.querySelectorAll('.chkBoxAssign').forEach(x => x.name = name);
       
        document.getElementById('user').value = name;
        document.getElementById('userId').value = id;
    }
    else if (type == 'remove') {
        debugger;
        //change button status
        openRemove.innerText = 'Remove Roles for ' + name;
        openRemove.classList = 'btn btn-warning';
        //assign the checkboxes the information based on the user
        document.querySelectorAll('.chkBoxRemove').forEach(x => x.name = name);
        //
        document.getElementById('userRemove').value = name;
        document.getElementById('userIdRemove').value = id;
       
    }   
}
function RoleSelection(name, type) {
    debugger;
    if (type == 'assign') {
        let checkboxes = document.querySelectorAll(`input[name="${name}"]:checked`);
        let roles = [];
        checkboxes.forEach((checkbox) => {
            roles.push(checkbox.value);
        })
        let newRoles = roles.join();
        document.getElementById('userRoles').value = newRoles;
        document.getElementById('confirmRoles').classList = 'btn btn-warning';
        document.getElementById('roleSelection').classList = 'btn btn-success disabled';
    }

    else if (type == 'remove') {
        let checkboxes = document.querySelectorAll(`input[name="${name}"]:checked`);
        let roles = [];
        checkboxes.forEach((checkbox) => {
            roles.push(checkbox.value);
        })
        let rolesToRemove = roles.join();
        document.getElementById('userRolesRemove').value = rolesToRemove;
        document.getElementById('confirmRolesRemoval').classList = 'btn btn-warning';
        document.getElementById('roleSelectionRemoval').classList = 'btn btn-success disabled';
    }
}

function ExpandDropdown(id) {
    debugger;
    document.getElementById(id).style.width = '400px';
}