function toggleEmployees(button, ProjectId) {
    const employeesContainer = document.getElementById("EmployeesContainer");
    employeesContainer.innerHTML = ""; // Clear the container for new content

    const isCollapsed = $('#collapseEmployees').hasClass('show');

    // Toggle collapse and button text
    if (isCollapsed) {
        $('#collapseEmployees').collapse('hide');
        button.innerHTML = `<i class="bi bi-person-badge me-2"></i> Employees`;
        button.setAttribute('aria-expanded', 'false');
    } else {
        $('#collapseEmployees').collapse('show');
        button.innerHTML = `<i class="bi bi-door-open me-2"></i> Close`;
        button.setAttribute('aria-expanded', 'true');

        $.ajax({
            url: "/Employees/GetEmployeesPerProject",
            type: "GET",
            data: { projectId: ProjectId },
            success: function (result) {
                console.log(result);
                if (result.length === 0) {
                    employeesContainer.innerHTML = `
                        <div class="alert alert-warning text-center mt-2">
                            <h4 class="alert-heading">No Employees!</h4>
                            <p class="mb-0">No Employees were added yet.</p>
                        </div>`;
                    return;
                }

                // Using a forEach to append employee cards
                result.forEach(st => {
                    const employeeCard = document.createElement("div");
                    employeeCard.className = "card employee-card d-flex justify-content-between align-items-center p-2 shadow-sm mx-auto";
                    employeeCard.style.width = "95%";
                    employeeCard.style.display = "flex";
                    employeeCard.style.flexDirection = "row";
                    employeeCard.style.transition = "transform 0.2s, box-shadow 0.2s";

                    employeeCard.onmouseover = function () {
                        this.classList.add('shadow-lg');
                        this.style.transform = 'scale(1.02)';
                    };
                    employeeCard.onmouseout = function () {
                        this.classList.remove('shadow-lg');
                        this.style.transform = 'scale(1)';
                    };

                    employeeCard.innerHTML = `
                        <div class="d-flex flex-column me-3">
                            <h5 class="mb-0 text-info">${st.name}</h5>
                            <p class="mb-0">Age: ${st.age}</p>
                            <p class="mb-0">Salary: $${st.salary}</p>
                        </div>
                        <img src="${imagesPath}/${st.image}" alt="${st.name}" style="width: 60px; height: 60px; object-fit: cover;">
                    `;
                    employeesContainer.appendChild(employeeCard);
                });
            },
            error: function () {
                console.log("Error fetching employees.");
                employeesContainer.innerHTML = `
                    <div class="alert alert-danger text-center mt-2">
                        <h4 class="alert-heading">Error!</h4>
                        <p class="mb-0">Failed to load employees. Please try again.</p>
                    </div>`;
            }
        });
    }
}
