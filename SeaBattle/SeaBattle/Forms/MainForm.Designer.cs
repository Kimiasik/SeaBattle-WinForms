using SeaBattle.Forms;
using System.Drawing;

namespace SeaBattle.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }
        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.arrangingControl = new SeaBattle.Forms.ArrangingControl();
            this.battleControl = new SeaBattle.Forms.BattleControl();
            this.startControl = new SeaBattle.Forms.StartControl();
            this.finishedControl = new SeaBattle.Forms.FinishedControl();
            this.SuspendLayout();
            // 
            // arrangingControl
            // 
            this.arrangingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arrangingControl.Location = new System.Drawing.Point(0, 0);
            this.arrangingControl.Name = "arrangingControl";
            this.arrangingControl.Size = new System.Drawing.Size(800, 600);
            this.arrangingControl.TabIndex = 1;
            // 
            // battleControl
            // 
            this.battleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.battleControl.Location = new System.Drawing.Point(0, 0);
            this.battleControl.Name = "battleControl";
            this.battleControl.Size = new System.Drawing.Size(800, 600);
            this.battleControl.TabIndex = 0;
            // 
            // startControl
            // 
            this.startControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startControl.Location = new System.Drawing.Point(0, 0);
            this.startControl.Name = "startControl";
            this.startControl.Size = new System.Drawing.Size(800, 600);
            this.startControl.TabIndex = 2;
            // 
            // finishedControl
            // 
            this.finishedControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.finishedControl.Location = new System.Drawing.Point(0, 0);
            this.finishedControl.Name = "finishedControl";
            this.finishedControl.Size = new System.Drawing.Size(800, 600);
            this.finishedControl.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.finishedControl);
            this.Controls.Add(this.startControl);
            this.Controls.Add(this.arrangingControl);
            this.Controls.Add(this.battleControl);
            this.MaximumSize = new System.Drawing.Size(816, 639);
            this.MinimumSize = new System.Drawing.Size(816, 639);
            this.Name = "MainForm";
            this.Text = "Морський бій";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BattleControl battleControl;
        private ArrangingControl arrangingControl;
        private StartControl startControl;
        private FinishedControl finishedControl;

    }
}

