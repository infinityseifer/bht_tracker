# 🧠 BHT Tracker – Student Behavior Tracking App

**Version:** `v1.2`  
**Live Version:** _[Deployed via Google Apps Script + GitHub Pages / Drive]_  
**Repository:** [github.com/infinityseifer/bht_tracker](https://github.com/infinityseifer/bht_tracker)

## 📌 Overview

**BHT Tracker** is a web-based tool built to streamline student behavior referral and intervention reporting for educators, counselors, and school staff. The app captures key behavioral data via dynamic forms and now integrates directly with Google Sheets — making it easier to collect, analyze, and act on intervention data.

---

## ✅ What's New in `v1.2`

### 📤 Google Sheets Integration (No Backend Required)
- Replaced traditional backend/database with **Google Apps Script** endpoint.
- Form responses are submitted directly to **Google Sheets**, including:
  - Behavior Referral Form (DrForm)
  - BHT Referral Form (BhtForm)

### 📊 Auto EDA (Exploratory Data Analysis)
- Added CSV upload and automatic data profiling module.
- Generates:
  - Descriptive statistics
  - Missing value reports
  - Frequency distributions
  - Quick plots (histograms, bar charts)

### 🖼️ UX/UI Enhancements
- "Preview Before Submit" feature for user review of form data.
- Live character counters for long-form fields (`Narrative`, `Observation`).
- Responsive design for tablet and desktop use.

---

## 📂 Project Structure
v1.2
A lightweight, school-facing web app to collect, preview, and store student behavior data via two forms:

Discipline Referral Form (dr-form.html)

BHT Referral Form (bht-form.html)

Users can preview entries, save previews as PDF, and submit directly to Google Sheets (no server/database needed for v1.2). A legacy .NET + MySQL path is included but optional.

What’s in v1.2

✅ Google Sheets integration (Apps Script) for both forms

✅ No-CORS submission using native HTML form posts (hidden iframe pattern)

✅ Consistent UI between DR & BHT forms (aligned checkboxes, larger labels where needed)

✅ Preview Before Submit + Save Preview as PDF (html2canvas + jsPDF)

🧪 (Planned) Auto EDA for CSV uploads (coming next)