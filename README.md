# FDB-Merge-Replace

A simple Windows Forms tool for **merging and replacing** `.fdb` files, designed for Eudemons Online (EO) client and server database editing.

---

## Features

- **Merge Mode:**  
  Add new records from File B (import) into File A (main), matching only selected fields and ignoring duplicates (by ID or selected key).
- **Replace Mode:**  
  Update selected fields in File A with data from File B, matching by selected key (e.g. `uID` or `id`). Only ticked fields will be replaced.
- **Tick Field Mapping:**  
  Choose which fields to merge or replace on both files. File B will auto-select fields based on what you tick in File A (prefix-insensitive).
- **Preview:**  
  Instantly preview the result before saving, in a simple table/grid.
- **Safe Saving:**  
  Output uses File A structure and keeps the header. No source file is ever overwritten.
- **Auto Select/Unselect:**  
  One-click select all or unselect all fields for easier mapping.

---

## How to Use

1. **Select Files:**  
   - File A: Your main (client/server) FDB file (data will be merged/replaced here).
   - File B: The import FDB file (source of new/updated data).

2. **Click `Load`**  
   - Fields and row counts will appear for both files.
   - File B will auto-tick only fields matching those ticked in File A.

3. **Choose Mode:**  
   - **Merge:** Add new rows from File B to File A (skip duplicates by key).
   - **Replace:** Overwrite selected fields in File A for matching records, by selected key.

4. **Select Fields:**  
   - Tick which fields to include.
   - In Replace mode, choose the key field from the dropdown (e.g. `uID`, `id`, etc.).

5. **Preview:**  
   - Click `Merge` or `Replace` to see the combined/updated data in the preview grid.

6. **Save:**  
   - When happy with the result, click `Save` and choose output file name.

---

## Notes

- File B fields auto-match by name suffix (ignores EO prefixes like `u`, `us`, `dw`, etc).
- Duplicates are detected by the chosen key field (first column is default if not found).
- Both Merge and Replace never modify your original files.
- Designed for `.fdb` format used in EO clients/servers; will not work with other formats.
