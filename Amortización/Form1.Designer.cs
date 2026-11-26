namespace Amortización
{
    partial class Form1
    {
   
        private System.ComponentModel.IContainer components = null;

       
       
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        private void InitializeComponent()
        {
            label1 = new Label();
            CajaAbono = new ComboBox();
            TextDeuda = new TextBox();
            label2 = new Label();
            TextInteres = new TextBox();
            label3 = new Label();
            CajaInteres = new ComboBox();
            label4 = new Label();
            TextPeriodos = new TextBox();
            BtnCalcular = new Button();
            BtnLimpiar = new Button();
            label5 = new Label();
            BtnCerrar = new Button();
            label6 = new Label();
            TextDato = new TextBox();
            CajaLista = new RichTextBox();
            BtnHistorial = new Button();
            label7 = new Label();
            TextGracia = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(202, 9);
            label1.Name = "label1";
            label1.Size = new Size(601, 50);
            label1.TabIndex = 0;
            label1.Text = "Programa de amortización";
            // 
            // CajaAbono
            // 
            CajaAbono.BackColor = SystemColors.InactiveBorder;
            CajaAbono.DropDownStyle = ComboBoxStyle.DropDownList;
            CajaAbono.FormattingEnabled = true;
            CajaAbono.Items.AddRange(new object[] { "Cuota fija(Calcular abono con plazo)", "Amortización constante(Abono variable)", "Abono fijo conocido(Calcular plazo)" });
            CajaAbono.Location = new Point(275, 133);
            CajaAbono.Name = "CajaAbono";
            CajaAbono.Size = new Size(279, 28);
            CajaAbono.TabIndex = 1;
            // 
            // TextDeuda
            // 
            TextDeuda.BackColor = SystemColors.InactiveBorder;
            TextDeuda.Location = new Point(266, 187);
            TextDeuda.Name = "TextDeuda";
            TextDeuda.Size = new Size(204, 27);
            TextDeuda.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe Script", 9F, FontStyle.Bold);
            label2.Location = new Point(12, 187);
            label2.Name = "label2";
            label2.Size = new Size(237, 25);
            label2.TabIndex = 3;
            label2.Text = "Monto del prestamo o deuda";
            // 
            // TextInteres
            // 
            TextInteres.BackColor = SystemColors.InactiveBorder;
            TextInteres.Location = new Point(237, 242);
            TextInteres.Name = "TextInteres";
            TextInteres.Size = new Size(91, 27);
            TextInteres.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe Script", 9F, FontStyle.Bold);
            label3.Location = new Point(12, 241);
            label3.Name = "label3";
            label3.Size = new Size(193, 25);
            label3.TabIndex = 5;
            label3.Text = "Ingrese taza de interés ";
            // 
            // CajaInteres
            // 
            CajaInteres.BackColor = SystemColors.InactiveBorder;
            CajaInteres.DropDownStyle = ComboBoxStyle.DropDownList;
            CajaInteres.FormattingEnabled = true;
            CajaInteres.Items.AddRange(new object[] { "Efectiva anual", "Efectiva del periodo" });
            CajaInteres.Location = new Point(334, 241);
            CajaInteres.Name = "CajaInteres";
            CajaInteres.Size = new Size(220, 28);
            CajaInteres.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe Script", 9F, FontStyle.Bold);
            label4.Location = new Point(12, 282);
            label4.Name = "label4";
            label4.Size = new Size(123, 25);
            label4.TabIndex = 7;
            label4.Text = "Pagos por año";
            // 
            // TextPeriodos
            // 
            TextPeriodos.BackColor = SystemColors.InactiveBorder;
            TextPeriodos.Location = new Point(165, 275);
            TextPeriodos.Name = "TextPeriodos";
            TextPeriodos.Size = new Size(163, 27);
            TextPeriodos.TabIndex = 8;
            // 
            // BtnCalcular
            // 
            BtnCalcular.BackColor = Color.Ivory;
            BtnCalcular.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnCalcular.Location = new Point(418, 491);
            BtnCalcular.Name = "BtnCalcular";
            BtnCalcular.Size = new Size(136, 61);
            BtnCalcular.TabIndex = 9;
            BtnCalcular.Text = "Calcular";
            BtnCalcular.UseVisualStyleBackColor = false;
            // 
            // BtnLimpiar
            // 
            BtnLimpiar.BackColor = Color.PaleGreen;
            BtnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnLimpiar.Location = new Point(591, 491);
            BtnLimpiar.Name = "BtnLimpiar";
            BtnLimpiar.Size = new Size(136, 61);
            BtnLimpiar.TabIndex = 10;
            BtnLimpiar.Text = "Limpiar";
            BtnLimpiar.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe Script", 9F, FontStyle.Bold);
            label5.Location = new Point(12, 136);
            label5.Name = "label5";
            label5.Size = new Size(238, 25);
            label5.TabIndex = 11;
            label5.Text = "Modalidad de amortización";
            // 
            // BtnCerrar
            // 
            BtnCerrar.BackColor = Color.Red;
            BtnCerrar.Font = new Font("Wide Latin", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnCerrar.Location = new Point(838, 502);
            BtnCerrar.Name = "BtnCerrar";
            BtnCerrar.Size = new Size(142, 68);
            BtnCerrar.TabIndex = 12;
            BtnCerrar.Text = "Cerrar";
            BtnCerrar.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe Script", 9F, FontStyle.Bold);
            label6.Location = new Point(12, 323);
            label6.Name = "label6";
            label6.Size = new Size(174, 25);
            label6.TabIndex = 13;
            label6.Text = "Número de periodos";
            // 
            // TextDato
            // 
            TextDato.BackColor = SystemColors.InactiveBorder;
            TextDato.Location = new Point(266, 316);
            TextDato.Name = "TextDato";
            TextDato.Size = new Size(153, 27);
            TextDato.TabIndex = 14;
            // 
            // CajaLista
            // 
            CajaLista.BackColor = SystemColors.Info;
            CajaLista.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CajaLista.Location = new Point(579, 66);
            CajaLista.Name = "CajaLista";
            CajaLista.ReadOnly = true;
            CajaLista.ScrollBars = RichTextBoxScrollBars.Vertical;
            CajaLista.Size = new Size(418, 398);
            CajaLista.TabIndex = 15;
            CajaLista.Text = "";
            // 
            // BtnHistorial
            // 
            BtnHistorial.BackColor = Color.Plum;
            BtnHistorial.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnHistorial.Location = new Point(221, 502);
            BtnHistorial.Name = "BtnHistorial";
            BtnHistorial.Size = new Size(137, 50);
            BtnHistorial.TabIndex = 16;
            BtnHistorial.Text = "Ver Historial";
            BtnHistorial.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe Script", 9F, FontStyle.Bold);
            label7.Location = new Point(12, 367);
            label7.Name = "label7";
            label7.Size = new Size(275, 25);
            label7.TabIndex = 17;
            label7.Text = "Periodos de gracia(solo interéses)";
            // 
            // TextGracia
            // 
            TextGracia.BackColor = SystemColors.InactiveBorder;
            TextGracia.Location = new Point(291, 364);
            TextGracia.Name = "TextGracia";
            TextGracia.Size = new Size(146, 27);
            TextGracia.TabIndex = 18;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1009, 596);
            Controls.Add(TextGracia);
            Controls.Add(label7);
            Controls.Add(BtnHistorial);
            Controls.Add(CajaLista);
            Controls.Add(TextDato);
            Controls.Add(label6);
            Controls.Add(BtnCerrar);
            Controls.Add(label5);
            Controls.Add(BtnLimpiar);
            Controls.Add(BtnCalcular);
            Controls.Add(TextPeriodos);
            Controls.Add(label4);
            Controls.Add(CajaInteres);
            Controls.Add(label3);
            Controls.Add(TextInteres);
            Controls.Add(label2);
            Controls.Add(TextDeuda);
            Controls.Add(CajaAbono);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox CajaAbono;
        private TextBox TextDeuda;
        private Label label2;
        private TextBox TextInteres;
        private Label label3;
        private ComboBox CajaInteres;
        private Label label4;
        private TextBox TextPeriodos;
        private Button BtnCalcular;
        private Button BtnLimpiar;
        private Label label5;
        private Button BtnCerrar;
        private Label label6;
        private TextBox TextDato;
        private RichTextBox CajaLista;
        private Button BtnHistorial;
        private Label label7;
        private TextBox TextGracia;
    }
}
