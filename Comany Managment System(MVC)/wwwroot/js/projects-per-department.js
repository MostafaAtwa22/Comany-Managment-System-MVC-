function toggleProjects(button, DeptId) {
    const projectsContainer = document.getElementById("ProjectsContainer");
    projectsContainer.innerHTML = ""; // Clear previous content

    const isCollapsed = $('#collapseProjects').hasClass('show');

    if (isCollapsed) {
        $('#collapseProjects').collapse('hide');
        button.innerHTML = `<i class="bi bi-building me-2"></i> Projects`;
        button.setAttribute('aria-expanded', 'false');
    } else {
        $('#collapseProjects').collapse('show');
        button.innerHTML = `<i class="bi bi-door-open me-2"></i> Close`;
        button.setAttribute('aria-expanded', 'true');

        // AJAX call to fetch projects for the given department
        $.ajax({
            url: "/Projects/GetProjectsPerDepartment",
            type: "GET",
            data: { deptId: DeptId },
            success: function (result) {
                console.log(result);
                if (result.length === 0) {
                    projectsContainer.innerHTML = `
                            <div class="d-flex justify-content-center align-items-center" style="width: 100%; height: 100%;">
                                <div class="alert alert-warning text-center mt-2" style="width: 95%;">
                                    <h4 class="alert-heading">No Projects!</h4>
                                    <p class="mb-0">No projects have been added yet.</p>
                                </div>
                            </div>`;
                    return;

                }

                for (let project of result) {
                    let projectCard = `
                        <div class="col-12 col-md-6 col-lg-6 d-flex justify-content-center mb-3"> <!-- Responsive columns for 2 per row on large screens -->
                            <div class="card project-card d-flex justify-content-between align-items-center p-2 shadow-sm" style="width: 100%; transition: transform 0.2s, box-shadow 0.2s;"
                                onmouseover="this.classList.add('shadow-lg'); this.style.transform='scale(1.02)'"
                                onmouseout="this.classList.remove('shadow-lg'); this.style.transform='scale(1)'">
                                <div class="d-flex flex-column me-3">
                                    <h5 class="mb-0 text-info">${project.name}</h5>
                                    <p class="mb-0">Location: ${project.location}</p>
                                    <p class="mb-0">Budget: $${project.budget}</p>
                                </div>
                            </div>
                        </div>`;
                    projectsContainer.innerHTML += projectCard;
                }
            },
            error: function () {
                console.log("Error fetching projects.");
                projectsContainer.innerHTML = `
                    <div class="alert alert-danger text-center mt-2">
                        <h4 class="alert-heading">Error!</h4>
                        <p class="mb-0">Failed to load projects. Please try again.</p>
                    </div>`;
            }
        });
    }
}
