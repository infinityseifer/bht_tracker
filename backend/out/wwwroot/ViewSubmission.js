let currentForm = "DrForm"; // Default form type

// Handle button toggle
document.getElementById("btnDrForm").addEventListener("click", () => {
  currentForm = "DrForm";
  setActiveButton("btnDrForm");
  clearResults();
});

document.getElementById("btnBhtForm").addEventListener("click", () => {
  currentForm = "BhtForm";
  setActiveButton("btnBhtForm");
  clearResults();
});

function setActiveButton(activeId) {
  document.getElementById("btnDrForm").classList.remove("active");
  document.getElementById("btnBhtForm").classList.remove("active");
  document.getElementById(activeId).classList.add("active");
}

function clearResults() {
  document.querySelector("#resultsTable tbody").innerHTML = "";
  document.getElementById("detailView").style.display = "none";
}

// Fetch filtered results
document.getElementById("applyFilters").addEventListener("click", async () => {
  const grade = document.getElementById("filterGrade").value;
  const start = document.getElementById("filterStartDate").value;
  const end = document.getElementById("filterEndDate").value;
  const studentName = document.getElementById("filterStudentName").value;

  let query = [];

  if (grade) query.push(`grade=${grade}`);
  if (start && end) query.push(`startDate=${start}&endDate=${end}`);
  if (studentName) query.push(`student=${encodeURIComponent(studentName)}`);

  const queryString = query.length ? `?${query.join("&")}` : "";

  try {
    const res = await fetch(`http://localhost:5210/api/${currentForm}${queryString}`);
    const data = await res.json();
    displayResults(data);
  } catch (err) {
    console.error("Error fetching data:", err);
    alert("Failed to load submissions.");
  }
});

// Populate table with results
function displayResults(data) {
  const tbody = document.querySelector("#resultsTable tbody");
  tbody.innerHTML = "";

  data.forEach((entry, index) => {
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${formatDate(entry.dateTime)}</td>
      <td>${entry.studentFirst} ${entry.studentLast}</td>
      <td>${entry.grade}</td>
      <td>${entry.homeroomTeacher || "-"}</td>
      <td>${entry.minorProblemBehavior || "-"}</td>
      <td>${entry.majorProblemBehavior || "-"}</td>
    `;

    row.addEventListener("click", () => {
      document.getElementById("detailView").style.display = "block";
      document.getElementById("detailContent").textContent = JSON.stringify(entry, null, 2);
    });

    tbody.appendChild(row);
  });
}

// Format date nicely
function formatDate(dateString) {
  const date = new Date(dateString);
  return date.toLocaleDateString() + " " + date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
}
