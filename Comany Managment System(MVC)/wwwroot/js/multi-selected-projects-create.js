$(document).ready(function () {
    // Initialize select2 for the first time
    $('.select2-multiple').select2({
        theme: "classic"
    });

    // Event handler for department selection
    $('#DepartmentId').change(function () {
        var deptId = $(this).val();
        $('#projectSelect').empty(); // Clear existing options in project select
        $('#noProjectsMessage').remove(); // Remove any existing message

        if (deptId) {
            $.ajax({
                url: '/Projects/GetProjectsPerDepartment', // Ensure this URL is correct
                type: 'GET',
                data: { deptId: deptId },
                success: function (data) {
                    console.log(data); // Log the data to check the response
                    if (data && data.length) {
                        // Populate projectSelect with the returned projects
                        $.each(data, function (index, project) {
                            $('#projectSelect').append(new Option(project.name, project.id));
                        });
                    } else {
                        // Display a message when no projects are found
                        $('#projectSelect').after('<div id="noProjectsMessage" class="text-danger mt-2">No projects found for this department.</div>');
                    }
                    // Re-initialize select2 after updating options
                    $('#projectSelect').select2({
                        theme: "classic"
                    });
                },
                error: function () {
                    $('#projectSelect').after('<div id="noProjectsMessage" class="text-danger mt-2">Error retrieving projects.</div>');
                    $('#projectSelect').select2({
                        theme: "classic"
                    });
                }
            });
        } else {
            $('#projectSelect').after('<div id="noProjectsMessage" class="text-danger mt-2">Please select a department first.</div>');
            $('#projectSelect').select2({
                theme: "classic"
            });
        }
    });

    // Ensure the text color is visible
    $('#projectSelect').css({
        'color': 'black',
        'background-color': 'white'
    });
});