using System.Drawing;
using System.Windows.Forms;

namespace ComputerStore.GUI.Helpers
{
    internal static class ManagementFormControls
    {
        public static FlowLayoutPanel CreateField(string title, Control editor, int width = 220)
        {
            var field = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Width = width,
                Height = 58,
                Margin = new Padding(4, 2, 8, 2)
            };
            field.Controls.Add(new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            });
            editor.Width = width - 8;
            editor.Margin = new Padding(0, 3, 0, 0);
            field.Controls.Add(editor);
            return field;
        }

        public static DataGridView CreateGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };
        }

        public static Button CreateButton(string text, int width = 105)
        {
            return new Button
            {
                Text = text,
                Width = width,
                Height = 32,
                Margin = new Padding(4),
                UseVisualStyleBackColor = true
            };
        }

        public static ComboBox CreateStatusCombo()
        {
            var combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            combo.Items.AddRange(new object[] { "Hoạt động", "Ngừng hoạt động" });
            combo.SelectedIndex = 0;
            return combo;
        }

        public static FlowLayoutPanel CreateToolbar()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 0, 0, 4)
            };
        }

        public static TableLayoutPanel CreateRoot()
        {
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            return root;
        }

        public static void SetColumnHeader(DataGridView grid, string property, string title)
        {
            if (grid.Columns[property] is DataGridViewColumn col)
                col.HeaderText = title;
        }
    }
}
