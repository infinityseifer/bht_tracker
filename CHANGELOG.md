v1.2 – “Sheets + Insights” (August 2025)
🚀 New Features

📤 Google Sheets Integration

Replaced backend database with a serverless form submission system using [Google Apps Script Web App].

Form data is posted directly to a designated Google Sheet.

📊 Automatic EDA for CSV Uploads

Python script (eda_pipeline.py) added to provide fast summary statistics, missing values, distributions, and visualizations from any uploaded dataset.

🖥️ UI/UX Enhancements

“Preview Before Submit”: Users can review entered data before final submission.

Live Character Counters: Added to long-text inputs like Narrative and Observation.

Cleaner HTML Structure: Improved semantic markup and section layout for forms.

Responsive Design Tweaks: Ensured better usability on tablets and laptops.

🔧 Codebase Changes

Removed .NET API Controllers and EF Core usage (backend logic no longer required).

Added new JavaScript logic for Google Sheets fetch() POST calls.

Reorganized /backend/wwwroot folder with:

index.html

dr-form.html

DrForm.js

bht-form.html

BhtForm.js

v1.1 – “Stability & Polish” (June 2025)

Polished form validations and visual layout.

Added PDF export of preview content using jsPDF and html2canvas.

Introduced dynamic field dependencies and updated CSS styles.

v1.0 – “Initial Launch” (May 2025)

Functional MVP: Behavior & BHT referral forms with local server backend.

Stored data in MySQL via EF Core.

Swagger UI for API testing.