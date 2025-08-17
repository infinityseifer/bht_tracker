// OPTIONAL: Add character count for Observation
const observationField = document.getElementById("Observation");
if (observationField) {
  observationField.addEventListener("input", function () {
    const counter = document.getElementById("ObservationCounter");
    counter.textContent = `${this.value.length} / 5000`;
  });
}

// Handle PREVIEW
document.getElementById("previewBtn").addEventListener("click", function () {
  const form = document.getElementById("BhtForm");
  const formData = new FormData(form);
  const data = Object.fromEntries(formData.entries());

  // Convert StudentID to number
  if (data.StudentID) {
    data.StudentID = parseInt(data.StudentID);
  }

  // Collect checkbox lists
  data.TierOne = Array.from(document.querySelectorAll('input[name="TierOne"]:checked')).map(cb => cb.value);
  data.TierTwo = Array.from(document.querySelectorAll('input[name="TierTwo"]:checked')).map(cb => cb.value);
  data.TierThree = Array.from(document.querySelectorAll('input[name="TierThree"]:checked')).map(cb => cb.value);
  data.StudentStrength = Array.from(document.querySelectorAll('input[name="StudentStrength"]:checked')).map(cb => cb.value);

  // Log to debug structure
  console.log("Previewed Payload:", JSON.stringify(data, null, 2));

  // Format preview HTML
  const previewSection = document.getElementById("previewSection");
  const previewContent = document.getElementById("previewContent");

  let previewHTML = "<ul style='list-style:none; padding-left:0'>";
  for (const [key, value] of Object.entries(data)) {
    const formattedKey = key.replace(/([A-Z])/g, ' $1').replace(/^./, str => str.toUpperCase());
    const displayValue = Array.isArray(value)
      ? value.map(v => v.replace(/\s+/g, ' ').trim()).join(', ')
      : value;

    previewHTML += `<li><strong>${formattedKey}:</strong><span>${displayValue}</span></li>`;
  }
  previewHTML += "</ul>";

  previewContent.innerHTML = previewHTML;
  previewSection.style.display = "block";

  // Store for submission
  window.previewFormData = data;
});

// Handle CONFIRM & SUBMIT
document.getElementById("confirmSubmitBtn").addEventListener("click", async function () {
  const data = window.previewFormData;

console.log("Final submission payload:", JSON.stringify(data, null, 2));

const requiredFields = [
  "Role", "StudentInitial", "StudentID", "StudentStatus", "TeacherName", "TeacherEmail",
  "ParentNotified", "MainConcern", "AdditionalConcern", "Observation",
  "FBA", "BehaviorData", "BehaviorTime", "BehaviorSubject"
];

for (const field of requiredFields) {
  if (!data[field]) {
    alert(`Missing required field: ${field}`);
    return;
  }
}

console.log("👉 Submitting payload:", JSON.stringify(data, null, 2));
  // Validate required fields
  for (const field of requiredFields) {
    if (!data[field]) {
      alert(`Missing required field: ${field}`);
      return;
    }
  }

  try {
    const response = await fetch("http://localhost:5210/api/BhtForm", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data)
    });

    const resultText = await response.text();
    console.log("Raw Server Response:", resultText);

    let result;
    try {
      result = JSON.parse(resultText);
    } catch {
      throw new Error("Invalid JSON returned from server.");
    }

    alert(result.message || "Form submitted successfully.");
    document.getElementById("BhtForm").reset();
    document.getElementById("previewSection").style.display = "none";
  } catch (err) {
    console.error("Submit Error:", err);
    alert("There was an error submitting the form.");
  }
});

// Save Preview as PDF
document.getElementById("savePdfBtn").addEventListener("click", async function () {
  const preview = document.getElementById("previewSection");

  if (!preview || preview.style.display === "none") {
    alert("Preview must be visible to save as PDF.");
    return;
  }

  const { jsPDF } = window.jspdf;
  const pdf = new jsPDF({ orientation: "portrait", unit: "pt", format: "a4" });

  await html2canvas(preview).then(canvas => {
    const imgData = canvas.toDataURL("image/png");
    const imgProps = pdf.getImageProperties(imgData);
    const pdfWidth = pdf.internal.pageSize.getWidth();
    const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;

    pdf.addImage(imgData, "PNG", 20, 20, pdfWidth - 40, pdfHeight);
    pdf.save("submission-preview.pdf");
  });
});
