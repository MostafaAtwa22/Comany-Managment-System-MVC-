$(document).ready(function () {
    // Initialize Select2
    $('#projectSelect').select2({
        theme: "classic"
    });

    // Pre-select assigned projects on page load
    var assignedProjects = JSON.parse($('#assignedProjects').val());
    console.log("Assigned Projects:", assignedProjects); // Debugging line
    var allProjects = []; // To hold all projects for the department

    // Function to populate the project select with projects
    function populateProjects(projects, assignedProjects) {
        $('#projectSelect').empty(); // Clear existing options
        if (projects.length) {
            $.each(projects, function (index, project) {
                var isSelected = assignedProjects.includes(project.id); // Check if the project is already assigned
                console.log("Project:", project.id, "Is Selected:", isSelected); // Debugging line
                $('#projectSelect').append(new Option(project.name, project.id, isSelected, isSelected));
            });
        } else {
            $('#projectSelect').after('<div id="noProjectsMessage" class="text-danger mt-2">No projects found for this department.</div>');
        }
        // Re-initialize select2
        $('#projectSelect').select2({
            theme: "classic"
        });
    }

    // Function to load projects for the current department
    function loadProjects(deptId) {
        $.ajax({
            url: '/Projects/GetProjectsPerDepartment',
            type: 'GET',
            data: { deptId: deptId },
            success: function (data) {
                allProjects = data; // Store all projects for the department
                console.log("All Projects:", allProjects); // Debugging line
                populateProjects(allProjects, assignedProjects); // Populate with assigned projects
            },
            error: function () {
                $('#projectSelect').after('<div id="noProjectsMessage" class="text-danger mt-2">Error retrieving projects.</div>');
            }
        });
    }

    // Populate the project select with all projects for the current department
    var currentDeptId = $('#DepartmentId').val();
    if (currentDeptId) {
        loadProjects(currentDeptId);
    }

    // Event handler for department selection to dynamically load projects
    $('#DepartmentId').change(function () {
        var deptId = $(this).val();
        $('#noProjectsMessage').remove(); // Remove any existing message
        if (deptId) {
            loadProjects(deptId); // Load projects for the selected department
        } else {
            $('#projectSelect').empty(); // Clear the project select if no department is selected
            $('#projectSelect').after('<div id="noProjectsMessage" class="text-danger mt-2">Please select a department first.</div>');
        }
    });
});