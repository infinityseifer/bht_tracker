// Live character count for Narrative
document.getElementById("Narrative").addEventListener("input", function () {
  const counter = document.getElementById("narrativeCounter");
  counter.textContent = `${this.value.length} / 2500`;
});

// PREVIEW before submit
document.getElementById("previewBtn").addEventListener("click", function () {
  const form = document.getElementById("DrForm");
  const formData = new FormData(form);
  const data = Object.fromEntries(formData.entries());  // ✅ Correctly use one FormData

  // Format datetime
  if (data.DateTime) {
    data.DateTime = new Date(data.DateTime).toISOString();
  }

  // Show preview
  const previewSection = document.getElementById("previewSection");
  const previewContent = document.getElementById("previewContent");

  let previewHTML = "<ul style='list-style:none; padding-left:0'>";
  for (const [key, value] of Object.entries(data)) {
    const formattedKey = key.replace(/([A-Z])/g, ' $1').replace(/^./, str => str.toUpperCase());
    previewHTML += `<li><strong>${formattedKey}:</strong> ${Array.isArray(value) ? value.join(', ') : value}</li>`;
  }
  previewHTML += "</ul>";

  previewContent.innerHTML = previewHTML;
  previewSection.style.display = "block";

  // Store for confirmed submit
  window.previewFormData = data;
});

// FINAL submit to Google Sheets
document.getElementById("confirmSubmitBtn").addEventListener("click", async function () {
  const data = window.previewFormData;

  try {
    const response = await fetch("https://script.google.com/macros/s/AKfycby2e-sbxaq9uYTLEh2L8GNVCRvJJAvE3lGWrqDxZbSOIsYgzpJf087H6UZ93zoKUSoc/exec", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data)
    });

    const resultText = await response.text();
    console.log("Response from Google Sheets:", resultText);
    alert("Form submitted successfully!");

    document.getElementById("DrForm").reset();
    document.getElementById("previewSection").style.display = "none";
  } catch (err) {
    console.error("Submit error:", err);
    alert("There was an error submitting the form.");
  }
});

// Reset form and hide preview
document.getElementById("resetBtn").addEventListener("click", function () {
  document.getElementById("DrForm").reset();
  document.getElementById("previewSection").style.display = "none";
});
