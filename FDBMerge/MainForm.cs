using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FDBMerge
{
    public partial class MainForm : Form
    {
        private List<FdbField> fieldsA = new();
        private List<List<object>> rowsA = new();
        private List<FdbField> fieldsB = new();
        private List<List<object>> rowsB = new();

        public MainForm()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitializeComponent();

            txtFileA.TextChanged += TxtFilePathChanged;
            txtFileB.TextChanged += TxtFilePathChanged;
            btnLoad.Enabled = false;

            rbMerge.CheckedChanged += RbMode_CheckedChanged;
            rbReplace.CheckedChanged += RbMode_CheckedChanged;
            // Mula-mula disable dua2 groupbox
            grpMerge.Enabled = false;
            grpReplace.Enabled = false;
            btnSave.Enabled = false;
            rbMerge.Enabled = false;
            rbReplace.Enabled = false;
            rbMerge.Checked = true;
            btnMerge.Enabled = false;
            btnSlctall.Enabled = false;
            btnUnslcall.Enabled = false;


            btnBrowseA.Click += BtnBrowseA_Click;
            btnBrowseB.Click += BtnBrowseB_Click;
            btnLoad.Click += BtnLoad_Click;
            btnMerge.Click += BtnMerge_Click;

            cmbKey.SelectedIndexChanged += cmbKey_SelectedIndexChanged;

            gridFieldA.CellValueChanged += (s, e) => UpdateTickedLabel(gridFieldA, lblFieldA, "A");
            gridFieldB.CellValueChanged += (s, e) => UpdateTickedLabel(gridFieldB, lblFieldB, "B");
            gridFieldA.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (gridFieldA.IsCurrentCellDirty)
                    gridFieldA.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            gridFieldB.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (gridFieldB.IsCurrentCellDirty)
                    gridFieldB.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void DisableKeyFieldInGrid(string keyField)
        {
            // Cari pada gridFieldA
            for (int i = 0; i < gridFieldA.Rows.Count; i++)
            {
                string fieldName = gridFieldA.Rows[i].Cells["Name"].Value?.ToString();
                if (fieldName == keyField)
                {
                    gridFieldA.Rows[i].Cells["Tick"].Value = true; // Force tick
                    gridFieldA.Rows[i].Cells["Tick"].ReadOnly = true; // Disable untick
                    gridFieldA.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    gridFieldA.Rows[i].Cells["Tick"].ReadOnly = false;
                    gridFieldA.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
            // Cari pada gridFieldB
            for (int i = 0; i < gridFieldB.Rows.Count; i++)
            {
                string fieldName = gridFieldB.Rows[i].Cells["Name"].Value?.ToString();
                if (fieldName == keyField)
                {
                    gridFieldB.Rows[i].Cells["Tick"].Value = true;
                    gridFieldB.Rows[i].Cells["Tick"].ReadOnly = true;
                    gridFieldB.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    gridFieldB.Rows[i].Cells["Tick"].ReadOnly = false;
                    gridFieldB.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }






        private void TxtFilePathChanged(object sender, EventArgs e)
        {
            // Only enable if both files exist and are not empty
            btnLoad.Enabled =
                !string.IsNullOrWhiteSpace(txtFileA.Text) &&
                !string.IsNullOrWhiteSpace(txtFileB.Text) &&
                File.Exists(txtFileA.Text) &&
                File.Exists(txtFileB.Text);
        }




        private void cmbKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            var keyField = cmbKey.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(keyField))
                DisableKeyFieldInGrid(keyField);
        }


        private void RbMode_CheckedChanged(object sender, EventArgs e)
        {
            grpMerge.Enabled = rbMerge.Checked;
            grpReplace.Enabled = rbReplace.Checked;
        }


        private void BtnBrowseA_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "FDB Files (*.fdb)|*.fdb|All Files (*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
                txtFileA.Text = ofd.FileName;
        }

        private void BtnBrowseB_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "FDB Files (*.fdb)|*.fdb|All Files (*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
                txtFileB.Text = ofd.FileName;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            // Load Field & Row A
            if (File.Exists(txtFileA.Text))
            {
                try { (fieldsA, rowsA, _) = FdbLoaderEPLStyle.Load(txtFileA.Text); }
                catch { fieldsA = new(); rowsA = new(); }
            }
            else { fieldsA = new(); rowsA = new(); }

            // Load Field & Row B
            if (File.Exists(txtFileB.Text))
            {
                try { (fieldsB, rowsB, _) = FdbLoaderEPLStyle.Load(txtFileB.Text); }
                catch { fieldsB = new(); rowsB = new(); }
            }
            else { fieldsB = new(); rowsB = new(); }

            ShowFieldList(fieldsA, gridFieldA, lblCountA, lblFieldA, "A", rowsA.Count);
            ShowFieldList(fieldsB, gridFieldB, lblCountB, lblFieldB, "B", rowsB.Count, fieldsA, gridFieldA);

            if (gridPreview != null) gridPreview.Rows.Clear();

            rbMerge.Enabled = true;
            rbReplace.Enabled = true;
            rbMerge.Checked = true;

            // Enable/disable groupbox ikut mode radio
            RbMode_CheckedChanged(null, null);

            btnSave.Enabled = false;
            btnMerge.Enabled = true;
            btnReplace.Enabled = true;

            // ------ Populate cmbKey (dropdown) ------
            cmbKey.Items.Clear();
            var keys = new List<string>();
            foreach (var fA in fieldsA)
            {
                if (fieldsB.Exists(fB => fB.Name.Equals(fA.Name, StringComparison.OrdinalIgnoreCase)))
                    keys.Add(fA.Name);
            }
            foreach (var k in keys) cmbKey.Items.Add(k);

            // Default: pilih uID kalau ada, else pilih yang pertama
            if (cmbKey.Items.Contains("uID"))
                cmbKey.SelectedItem = "uID";
            else if (cmbKey.Items.Count > 0)
                cmbKey.SelectedIndex = 0;


            if (cmbKey.SelectedItem != null)
                DisableKeyFieldInGrid(cmbKey.SelectedItem.ToString());

        }



        // Papar field nama + tick + update label item & ticked
        private void ShowFieldList(
            List<FdbField> fields,
            DataGridView grid,
            Label lblCount,
            Label lblField,
            string fileTag,
            int totalItem,
            List<FdbField> refFieldsTicked = null,
            DataGridView refGrid = null
        )
        {
            grid.Columns.Clear();
            grid.Rows.Clear();

            var colCheck = new DataGridViewCheckBoxColumn { HeaderText = "✔", Name = "Tick", Width = 30 };
            grid.Columns.Add(colCheck);
            grid.Columns.Add("Name", "Field Name");

            for (int i = 0; i < fields.Count; i++)
            {
                bool tick = false;
                if (refFieldsTicked != null && refGrid != null)
                {
                    string nameB = StripPrefix(fields[i].Name);

                    // Loop semua row di gridFieldA dan cari yang match
                    for (int j = 0; j < refFieldsTicked.Count; j++)
                    {
                        string nameA = StripPrefix(refFieldsTicked[j].Name);
                        bool tickedA = false;
                        if (refGrid.Rows[j].Cells["Tick"].Value is bool b) tickedA = b;

                        // Kalau ticked dan nama belakang sama (ignore prefix)
                        if (tickedA && nameA == nameB)
                        {
                            tick = true;
                            break;
                        }
                    }
                }
                else
                {
                    tick = true;
                }

                grid.Rows.Add(tick, fields[i].Name);
            }


            grid.AutoResizeColumns();
            lblCount.Text = $"Total Item: {totalItem}";
            UpdateTickedLabel(grid, lblField, fileTag);
        }

        private static readonly string[] Prefixes = {
    "us", "uc", "un", "dw", "sz", "u", "s", "n", "b"
};
        private string StripPrefix(string fieldName)
        {
            fieldName = fieldName.Trim();
            // Cari prefix yang match PALING PANJANG dulu
            string lower = fieldName.ToLower();
            string best = fieldName;
            int longest = 0;
            foreach (var pre in Prefixes)
            {
                if (lower.StartsWith(pre) && pre.Length > longest)
                {
                    best = fieldName.Substring(pre.Length);
                    longest = pre.Length;
                }
            }
            return best.ToLower();
        }



        // Update label atas grid (tick count)
        private void UpdateTickedLabel(DataGridView grid, Label lbl, string tag)
        {
            int ticked = 0;
            foreach (DataGridViewRow row in grid.Rows)
                if (row.Cells["Tick"].Value is bool b && b) ticked++;
            lbl.Text = $"Fields in File {tag} ({ticked} ticked)";
        }

        // ---- MERGE LOGIC BUTTON ----
        private void BtnMerge_Click(object sender, EventArgs e)
        {
            var matchedFields = GetMatchedTickedFieldIndexes();
            if (matchedFields.Count == 0)
            {
                MessageBox.Show("No field matching & ticked both side!", "Merge", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cari nama/nombor index ID column (first field, atau cari "id" ignore case)
            int idIdxA = fieldsA.FindIndex(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase));
            if (idIdxA < 0) idIdxA = 0;
            int idIdxB = fieldsB.FindIndex(f => f.Name.Equals("id", StringComparison.OrdinalIgnoreCase));
            if (idIdxB < 0) idIdxB = 0;

            // Set ID sedia ada
            var existingIDs = new HashSet<string>(rowsA.Count);
            foreach (var row in rowsA)
                existingIDs.Add(row[idIdxA]?.ToString() ?? "");

            int inserted = 0;
            foreach (var rowB in rowsB)
            {
                string idB = rowB[idIdxB]?.ToString() ?? "";
                if (existingIDs.Contains(idB)) continue; // duplicate, skip

                // Bina row baru ikut structure A
                var newRow = new object[fieldsA.Count];
                for (int i = 0; i < fieldsA.Count; i++)
                    newRow[i] = GetDefaultValue(fieldsA[i].Type);

                // Map value ikut matchedFields
                foreach (var (idxA, idxB) in matchedFields)
                    newRow[idxA] = rowB[idxB];

                rowsA.Add(new List<object>(newRow));
                existingIDs.Add(idB);
                inserted++;
            }

            // Preview hasil merge (data grid)
            ShowPreviewData(rowsA, fieldsA, gridPreview);

            MessageBox.Show($"Merge Complete! {inserted} new item insert.", "Merge Complete");
            lblCountA.Text = $"Total Item: {rowsA.Count}";
            btnSave.Enabled = true; // <-- ENABLE SAVE BUTTON!

        }

        // Return senarai (idxA, idxB) field yang ticked & nama sama
        private List<(int idxA, int idxB)> GetMatchedTickedFieldIndexes()
        {
            var matched = new List<(int, int)>();
            for (int i = 0; i < gridFieldA.Rows.Count; i++)
            {
                bool tickA = Convert.ToBoolean(gridFieldA.Rows[i].Cells["Tick"].Value ?? false);
                string nameA = gridFieldA.Rows[i].Cells["Name"].Value?.ToString();
                if (!tickA || string.IsNullOrEmpty(nameA)) continue;

                for (int j = 0; j < gridFieldB.Rows.Count; j++)
                {
                    bool tickB = Convert.ToBoolean(gridFieldB.Rows[j].Cells["Tick"].Value ?? false);
                    string nameB = gridFieldB.Rows[j].Cells["Name"].Value?.ToString();
                    if (!tickB || string.IsNullOrEmpty(nameB)) continue;
                    if (nameA == nameB)
                    {
                        matched.Add((i, j));
                        break;
                    }
                }
            }
            return matched;
        }

        // Untuk row kosong, fallback ikut type
        private object GetDefaultValue(byte type)
        {
            return type switch
            {
                1 => (byte)0,
                2 => (short)0,
                3 => (ushort)0,
                4 => 0,
                5 => 0u,
                6 => 0f,
                7 => 0d,
                8 => 0L,
                9 => 0UL,
                10 => "",
                _ => null
            };
        }

        // Preview result dalam DataGridView (gridPreview)
        private void ShowPreviewData(List<List<object>> rows, List<FdbField> fields, DataGridView grid)
        {
            grid.Columns.Clear();
            foreach (var f in fields)
                grid.Columns.Add(f.Name, f.Name);

            grid.Rows.Clear();
            foreach (var row in rows)
                grid.Rows.Add(row.ToArray());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (fieldsA == null || fieldsA.Count == 0 || rowsA == null || rowsA.Count == 0)
            {
                MessageBox.Show("No data to keep!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dlg = new SaveFileDialog
            {
                Filter = "FDB Files (*.fdb)|*.fdb|All Files (*.*)|*.*",
                FileName = "merged.fdb"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                // Simpan header asal file A (optional, boleh pass null kalau tak simpan)
                byte[] header = null;
                if (File.Exists(txtFileA.Text))
                {
                    using var fs = new FileStream(txtFileA.Text, FileMode.Open, FileAccess.Read);
                    header = new byte[0x20];
                    fs.Read(header, 0, 0x20);
                }

                FdbLoaderEPLStyle.Save(dlg.FileName, fieldsA, rowsA, header);
                MessageBox.Show("Save completed!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save file: " + ex.Message, "Error");
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            // Pastikan user pilih key
            if (cmbKey.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Matching Key!", "Replace");
                return;
            }
            string keyField = cmbKey.SelectedItem.ToString();

            // Cari index key pada fieldsA dan fieldsB
            int keyIdxA = fieldsA.FindIndex(f => f.Name.Equals(keyField, StringComparison.OrdinalIgnoreCase));
            int keyIdxB = fieldsB.FindIndex(f => f.Name.Equals(keyField, StringComparison.OrdinalIgnoreCase));
            if (keyIdxA < 0 || keyIdxB < 0)
            {
                MessageBox.Show("Matching Key not found on both file!", "Replace");
                return;
            }

            // Dapatkan matched & ticked fields
            var matchedFields = GetMatchedTickedFieldIndexes();
            if (matchedFields.Count == 0)
            {
                MessageBox.Show("No field matching & ticked both side!", "Replace");
                return;
            }

            // Bina dictionary utk fast lookup di rowsB
            var dictB = new Dictionary<string, List<object>>();
            foreach (var rowB in rowsB)
            {
                var keyB = rowB[keyIdxB]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(keyB))
                    dictB[keyB] = rowB;
            }

            int updated = 0;
            foreach (var rowA in rowsA)
            {
                var keyA = rowA[keyIdxA]?.ToString() ?? "";
                if (string.IsNullOrEmpty(keyA)) continue;

                if (dictB.TryGetValue(keyA, out var rowB))
                {
                    // Replace field yg ticked je
                    foreach (var (idxA, idxB) in matchedFields)
                    {
                        // Skip key field (jgn replace)
                        if (idxA == keyIdxA) continue;
                        rowA[idxA] = rowB[idxB];
                    }
                    updated++;
                }
            }

            // Preview hasil replace
            ShowPreviewData(rowsA, fieldsA, gridPreview);

            MessageBox.Show($"Replace Complete! {updated} item updated.", "Replace Complete");
            lblCountA.Text = $"Total Item: {rowsA.Count}";
            btnSave.Enabled = true;
        }

        private void btnSlctall_Click(object sender, EventArgs e)
        {
            SelectAllGrid(gridFieldA);
            SelectAllGrid(gridFieldB);
        }

        private void SelectAllGrid(DataGridView grid)
        {
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                // Hanya update kalau bukan ReadOnly
                if (!grid.Rows[i].Cells["Tick"].ReadOnly)
                    grid.Rows[i].Cells["Tick"].Value = true;
            }
        }


        private void btnUnslcall_Click(object sender, EventArgs e)
        {
            UnselectAllGrid(gridFieldA);
            UnselectAllGrid(gridFieldB);
        }

        private void UnselectAllGrid(DataGridView grid)
        {
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                // Hanya update kalau bukan ReadOnly
                if (!grid.Rows[i].Cells["Tick"].ReadOnly)
                    grid.Rows[i].Cells["Tick"].Value = false;
            }
        }

    }

    public class FdbField
    {
        public string Name;
        public byte Type;
    }

    public static class FdbLoaderEPLStyle
    {
        // Return: (field, row, header)
        public static (List<FdbField>, List<List<object>>, byte[]) Load(string path)
        {
            var data = File.ReadAllBytes(path);
            const int HEADER_SIZE = 0x20;

            byte[] header = new byte[HEADER_SIZE];
            Array.Copy(data, 0, header, 0, HEADER_SIZE);

            int fieldCount = BitConverter.ToInt32(data, 0x14);
            int rowCount = BitConverter.ToInt32(data, 0x18);
            int textLen = BitConverter.ToInt32(data, 0x1C);
            int textBase = data.Length - textLen;

            var gbk = Encoding.GetEncoding("GBK");
            List<string> labels = new();
            int ptr = textBase;
            for (int i = 0; i < fieldCount; i++)
            {
                int start = ptr;
                while (ptr < data.Length && data[ptr] != 0) ptr++;
                labels.Add(gbk.GetString(data, start, ptr - start));
                ptr++;
            }
            var fields = new List<FdbField>();
            for (int i = 0; i < fieldCount; i++)
            {
                int fieldOffset = HEADER_SIZE + i * 5;
                byte type = data[fieldOffset];
                fields.Add(new FdbField { Name = labels[i], Type = type });
            }

            int ptrTableOffset = HEADER_SIZE + fieldCount * 5;
            var rowPtrs = new List<int>();
            for (int i = 0; i < rowCount; i++)
            {
                int recPos = ptrTableOffset + i * 8;
                int recPtr = BitConverter.ToInt32(data, recPos + 4);
                rowPtrs.Add(recPtr);
            }

            var rows = new List<List<object>>(rowCount);
            foreach (var rowPtr in rowPtrs)
            {
                if (rowPtr <= 0 || rowPtr == 0xD6000000)
                {
                    rows.Add(new List<object>(new string[fields.Count]));
                    continue;
                }
                int pos = rowPtr;
                var values = new List<object>(fields.Count);
                for (int f = 0; f < fields.Count; f++)
                {
                    var field = fields[f];
                    object val = null;
                    switch (field.Type)
                    {
                        case 1: val = data[pos]; pos += 1; break;
                        case 2: val = BitConverter.ToInt16(data, pos); pos += 2; break;
                        case 3: val = (ushort)BitConverter.ToInt16(data, pos); pos += 2; break;
                        case 4: val = BitConverter.ToInt32(data, pos); pos += 4; break;
                        case 5: val = (uint)BitConverter.ToInt32(data, pos); pos += 4; break;
                        case 6: val = BitConverter.ToSingle(data, pos); pos += 4; break;
                        case 7: val = BitConverter.ToDouble(data, pos); pos += 8; break;
                        case 8: val = BitConverter.ToInt64(data, pos); pos += 8; break;
                        case 9: val = (ulong)BitConverter.ToInt64(data, pos); pos += 8; break;
                        case 10:
                            int strPtr = BitConverter.ToInt32(data, pos);
                            int strAddr = textBase + strPtr;
                            val = "";
                            if (strAddr >= 0 && strAddr < data.Length)
                            {
                                int end = strAddr;
                                while (end < data.Length && data[end] != 0) end++;
                                val = gbk.GetString(data, strAddr, end - strAddr);
                            }
                            pos += 4;
                            break;
                        default:
                            val = ""; break;
                    }
                    values.Add(val);
                }
                rows.Add(values);
            }
            return (fields, rows, header);
        }

        // Save siap
        public static void Save(string path, List<FdbField> fields, List<List<object>> rows, byte[] header)
        {
            const int HEADER_SIZE = 0x20;
            var gbk = Encoding.GetEncoding("GBK");
            int fieldCount = fields.Count;
            int rowCount = rows.Count;

            // ===== 1. BUILD TEXT POOL =====
            List<byte> textBytes = new();
            Dictionary<string, int> stringPointerDict = new();

            // Field names
            foreach (var f in fields)
            {
                var raw = gbk.GetBytes(f.Name ?? "");
                textBytes.AddRange(raw);
                textBytes.Add(0);
            }
            // Type 10 values
            foreach (var row in rows)
            {
                for (int f = 0; f < fields.Count; f++)
                {
                    if (fields[f].Type == 10)
                    {
                        string s = row[f]?.ToString() ?? "";
                        if (!stringPointerDict.ContainsKey(s))
                        {
                            stringPointerDict[s] = textBytes.Count;
                            var raw = gbk.GetBytes(s);
                            textBytes.AddRange(raw);
                            textBytes.Add(0);
                        }
                    }
                }
            }

            int textLen = textBytes.Count;
            int textBase = HEADER_SIZE + fieldCount * 5 + rowCount * 8;
            int estimatedRowBytes = rows.Count * fields.Count * 8 + 1024;
            byte[] outBuf = new byte[textBase + estimatedRowBytes + textLen];

            // === 3. Salin header asal atau buat kosong ===
            if (header != null && header.Length >= HEADER_SIZE)
                Array.Copy(header, outBuf, HEADER_SIZE);
            else
                Array.Clear(outBuf, 0, HEADER_SIZE);

            BitConverter.GetBytes(fieldCount).CopyTo(outBuf, 0x14);
            BitConverter.GetBytes(rowCount).CopyTo(outBuf, 0x18);
            BitConverter.GetBytes(textLen).CopyTo(outBuf, 0x1C);

            // === 4. Field types ===
            for (int i = 0; i < fieldCount; i++)
            {
                int fieldOffset = HEADER_SIZE + i * 5;
                outBuf[fieldOffset] = fields[i].Type;
            }

            // === 5. Bina pointer table dan tulis row data ===
            int ptrTableOffset = HEADER_SIZE + fieldCount * 5;
            int rowDataBase = ptrTableOffset + rowCount * 8;
            int rowPtr = rowDataBase;

            for (int i = 0; i < rowCount; i++)
            {
                int ptrPos = ptrTableOffset + i * 8;

                // **Ambil ID dari kolum pertama row, default 0**
                int rowId = 0;
                if (!(rows[i].All(x => x == null || x.ToString() == "")))
                {
                    object idVal = rows[i][0];
                    if (idVal != null)
                    {
                        if (idVal is int) rowId = (int)idVal;
                        else int.TryParse(idVal.ToString(), out rowId);
                    }
                }
                BitConverter.GetBytes(rowId).CopyTo(outBuf, ptrPos); // +0: ID

                // --- Check if this row is "empty" (semua kosong/null) ---
                bool isEmpty = rows[i].All(x => x == null || x.ToString() == "");
                if (isEmpty)
                {
                    BitConverter.GetBytes(0).CopyTo(outBuf, ptrPos + 4); // +4: offset = 0 utk kosong
                    continue;
                }

                BitConverter.GetBytes(rowPtr).CopyTo(outBuf, ptrPos + 4); // +4: offset ke row data

                // === Tulis row data, ikut format asal ===
                for (int f = 0; f < fieldCount; f++)
                {
                    var field = fields[f];
                    object val = rows[i][f];
                    switch (field.Type)
                    {
                        case 1:
                            outBuf[rowPtr++] = Convert.ToByte(val ?? 0);
                            break;
                        case 2:
                            BitConverter.GetBytes(Convert.ToInt16(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 2;
                            break;
                        case 3:
                            BitConverter.GetBytes(Convert.ToUInt16(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 2;
                            break;
                        case 4:
                            BitConverter.GetBytes(Convert.ToInt32(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 4;
                            break;
                        case 5:
                            BitConverter.GetBytes(Convert.ToUInt32(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 4;
                            break;
                        case 6:
                            BitConverter.GetBytes(Convert.ToSingle(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 4;
                            break;
                        case 7:
                            BitConverter.GetBytes(Convert.ToDouble(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 8;
                            break;
                        case 8:
                            BitConverter.GetBytes(Convert.ToInt64(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 8;
                            break;
                        case 9:
                            BitConverter.GetBytes(Convert.ToUInt64(val ?? 0)).CopyTo(outBuf, rowPtr);
                            rowPtr += 8;
                            break;
                        case 10:
                            string s = val?.ToString() ?? "";
                            int strPtr = stringPointerDict.ContainsKey(s) ? stringPointerDict[s] : 0;
                            BitConverter.GetBytes(strPtr).CopyTo(outBuf, rowPtr);
                            rowPtr += 4;
                            break;
                        default:
                            BitConverter.GetBytes(0).CopyTo(outBuf, rowPtr);
                            rowPtr += 4;
                            break;
                    }
                }
            }

            // === 6. Sambung text pool ===
            Array.Copy(textBytes.ToArray(), 0, outBuf, rowPtr, textBytes.Count);
            int realTotalLen = rowPtr + textBytes.Count;

            // === 7. Simpan file, potong betul² size ===
            File.WriteAllBytes(path, outBuf.AsSpan(0, realTotalLen).ToArray());
        }

    }
}
