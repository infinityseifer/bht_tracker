PR22: Stabilize Sheets integration, unify UI, docs update (v1.2.1)

## What & Why
- Centralizes Google Apps Script endpoints in one config to simplify env switching
- Improves form submission UX (loading state, validation, clearer errors)
- Unifies checkbox alignment + label sizing across DR & BHT
- Guards BHT against unintended MySQL writes while we migrate to Sheets
- Updates README with GAS deploy + local testing steps
- Bumps version to v1.2.1

## Changes
- /wwwroot/js/config.js (NEW): { GAS_DR_URL, GAS_BHT_URL }
- /wwwroot/DrForm.js: uses config, adds loading/alerts, minor cleanup
- /wwwroot/BhtForm.js: uses config, consistent preview + submit flow
- /wwwroot/bht-form.html + dr-form.html: consistent classes for checkboxes & labels
- Program.cs: comment/remove unused MySQL for BHT path
- README.md: new “Google Sheets setup”, “Testing locally”, “Deploy (Drive/GAS)” sections
- CHANGELOG.md: add v1.2.1

## Screenshots
_(attach 2–3 small screenshots of DR/BHT previews & checkboxes)_

## How to Test
1. Create/confirm GAS web apps for DR & BHT; paste URLs into `/wwwroot/js/config.js`.
2. Run backend & open:
   - `http://localhost:5210/dr-form.html`
   - `http://localhost:5210/bht-form.html`
3. Fill forms → Preview → Confirm; verify rows appear in the correct Sheets.
4. Toggle required-field misses; expect friendly alerts.
5. Confirm checkbox alignment and larger “Student Strength” label.

## Checklist
- [ ] DR submits to the DR sheet successfully
- [ ] BHT submits to the BHT sheet successfully
- [ ] No console errors on either form
- [ ] README updated
- [ ] CHANGELOG updated to v1.2.1
- [ ] Version banner/footer (if any) shows v1.2.1

## Risks & Rollback
- Low risk; revert by pointing forms back to previous GAS URLs and restoring prior JS.
- Rollback plan: `git revert -m 1 <merge-commit>` or redeploy v1.2 tag.
