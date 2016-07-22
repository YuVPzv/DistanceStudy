﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace DrawG {
    public partial class MainForm : Form {
        /// <summary>
        /// Переменная, указывающая состояние отрисовки линии
        /// </summary>
        public bool lineControl { get; set; }
        public Point PointVarFirst { get; set; }
        public Point PointVarTwo { get; set; }
        public MainForm()
        {
            InitializeComponent();
            PropertyPoint3D.PointOfPlan1_X0Y_Var = null;
            PropertyPoint3D.PointOfPlan2_X0Z_Var = null;
            PropertyPoint3D.PointOfPlan3_Y0Z_Var = null;
            lineControl = false;

        }
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.pictureBoxWidth = PictureBox1.Width;
            DrawObjectsToPictureBox.pictureBoxHeight = PictureBox1.Height;
            // 1. Создание независимой картинки и поверхности рисования Graphics активных объектов
            DrawObjectsToPictureBox.BitmapActive = new Bitmap(PictureBox1.ClientSize.Width, PictureBox1.ClientSize.Height, PixelFormat.Format24bppRgb);
            DrawObjectsToPictureBox.BitmapActive.MakeTransparent();
            GraphicsTools.CreateGraphicsByImage(DrawObjectsToPictureBox.BitmapActive, out DrawObjectsToPictureBox.GraphicsActive);

            // 2. Создание независимой картинки и поверхности рисования Graphics объектов фона
            DrawObjectsToPictureBox.BitmapBack = new Bitmap(PictureBox2.ClientSize.Width, PictureBox2.ClientSize.Height, PixelFormat.Format24bppRgb);
            DrawObjectsToPictureBox.BitmapBack.MakeTransparent();
            GraphicsTools.CreateGraphicsByImage(DrawObjectsToPictureBox.BitmapBack, out DrawObjectsToPictureBox.GraphicsBack);

            // 3. Заполнить подложку (фон)
            DrawObjectsToPictureBox.GraphicsBack_Create(PictureBox1, PictureBox2);

            // 5. Отрисовка заданных внешних объектов
            DrawObjectsToPictureBox.OutObjects_Var.Collection_Draw(PictureBox1);
        }
        /// <summary>
        /// Включение/выключение привязки к сетке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabel7_CursorToGridFixation_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel7_CursorToGridFixation.BorderStyle == Border3DStyle.RaisedInner) {
                toolStripStatusLabel7_CursorToGridFixation.BorderStyle = Border3DStyle.SunkenOuter;
                CursorTools.CursorCreate(30, 30, Color.Black);
                Cursor_Draw.CursorToGridFixation = true;

            } else {
                toolStripStatusLabel7_CursorToGridFixation.BorderStyle = Border3DStyle.RaisedInner;
                Cursor_Draw.GridCursor.Dispose();
                Cursor_Draw.CursorToGridFixation = false;
            }
        }
        /// <summary>
        /// Действие при нажатии левой кнопки мыши на PictureBox1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_MouseDown(object sender, EventArgs e)
        {
            ControlDraw.UserMouseClick = PictureBox1.PointToClient(Control.MousePosition);
            Point op = (Point)ControlDraw.UserMouseClick;
            if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetPoint) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetPoint3D) {
                PropertyPoint3D.FlagPoint_Point3D = true;
                PropertyPoint3D.FlagPoint3D_CreateStop = false;
                DoEvents_Point3D();
                PropertyPoint3D.FlagPoint3D_CreateStop = true;
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetPointPlan1X0Y) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
                if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                    ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                }
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetPointPlan2X0Z) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
                if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                    ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                }
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetPointPlan3Y0Z) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
                if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                    ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                }
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SelectObject) { // Выбор и изменение активных объектов
                ControlDraw.Objects_SelectAndFire(PictureBox1);
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetPoint3DByPointOfPlan) {
                ControlDraw.Objects_SelectAndFirePointOfPlane(PictureBox1);
                if (PropertyPoint3D.FlagPoint_Point3D) {
                } else {
                    DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.None;
                    PictureBox1.Cursor = Cursors.Default;
                }
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.DeleteSelectObjects) // Удалить выбранные объекты
            {
                ControlDraw.UserMouseClick = PictureBox1.PointToClient(Control.MousePosition); // Фиксация положения курсора при MouseDown
                ControlDraw.Objects_SelectAndDelete(PictureBox1, PictureBox2); // Удалить выбранные объект
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.DeleteObjects) {
                ControlDraw.UserMouseClick = PictureBox1.PointToClient(Control.MousePosition); // Фиксация положения курсора при MouseDown
                ControlDraw.Objects_SelectAndFire(PictureBox1); // Подсветить удаляемый объект
                ControlDraw.Objects_SelectAndDelete(PictureBox1, PictureBox2); // Удалить выбранный объект
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetLine) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetLine3D) {
                PropertyLine3D.FlagPoint3DCreateStop = false;
                PropertyLine3D.FlagLine3D = true;
                DoEvents_Line3D();
                PropertyLine3D.FlagPoint3DCreateStop = false;
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetLinePlan1X0Y) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
                if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                    ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                }
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetLinePlan2X0Z) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
                if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                    ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                }
            } else if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetLinePlan3Y0Z) {
                ControlDraw.Objects_DrawAndAdd(PictureBox1);
                if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                    ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                }
            }
            PictureBox1.Image = (Image)DrawObjectsToPictureBox.BitmapActive.Clone(); // Передача картинки активных объектов в PictureBox_Source
            PictureBox1.Refresh(); // Отрисовка Graphics заново в PictureBox_Source.Image с сохранением всех нарисованных в Graphics объектов
        }
        /// <summary>
        /// Перемещение курсора по PictureBox1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor_Draw.CursorPointToGridMove(PictureBox1); // Привязка к сетке

            // Отображение координат текущего положения курсора
            toolStripStatusLabel1_X_Value.Text = (PictureBox1.PointToClient(Cursor.Position).X - ControlDraw.GridDraw_Var.GridCenter.X).ToString();
            toolStripStatusLabel1_Y_Value.Text = (PictureBox1.PointToClient(Cursor.Position).Y - ControlDraw.GridDraw_Var.GridCenter.Y).ToString();
        }
        /// <summary>
        /// Отрисовка активной 2D точки 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Point2D_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetPoint; // Задание режима отрисовки активной точки
            PictureBox1.Cursor = Cursors.Cross;
        }
        /// <summary>
        /// Включить/выключить сетку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabel4_StatusGrid_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel4_StatusGrid.BorderStyle == Border3DStyle.RaisedInner) {
                toolStripStatusLabel4_StatusGrid.BorderStyle = Border3DStyle.SunkenOuter;
                ControlDraw.GridDraw_Var.GridFlagDraw = true;
                ControlDraw.GridDraw_Var.GridDefaultSetting.FlagDraw = true; // Передача значения в параметры настройки
                DrawObjectsToPictureBox.GraphicsBack_Add(PictureBox1, PictureBox2);
            } else {
                toolStripStatusLabel4_StatusGrid.BorderStyle = Border3DStyle.RaisedInner;
                ControlDraw.GridDraw_Var.GridFlagDraw = false;
                ControlDraw.GridDraw_Var.GridDefaultSetting.FlagDraw = false; // Передача значения в параметры настройки
                DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
            }
        }
        /// <summary>
        /// Включить оси
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStatusLabel5_SatusAxis_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel5_SatusAxis.BorderStyle == Border3DStyle.RaisedInner) {
                toolStripStatusLabel5_SatusAxis.BorderStyle = Border3DStyle.SunkenOuter;
                ControlDraw.AxisDraw_Var.showAxisXYZ = true;
                DrawObjectsToPictureBox.GraphicsBack_Add(PictureBox1, PictureBox2);
            } else {
                toolStripStatusLabel5_SatusAxis.BorderStyle = Border3DStyle.RaisedInner;
                ControlDraw.AxisDraw_Var.showAxisXYZ = false;
                DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
            }
        }
        /// <summary>
        /// Выбрать графические объекты ( Кнопка "Выбрать" )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SelectObject; // Задание режима выбора активного объекта
            PictureBox1.Cursor = Cursors.Hand; // Указание вида курсора
            ControlDraw.UserMouseClick = null; // Обнуление для указания следующего объекта
        }
        /// <summary>
        /// Удалить выбранный объект ( Кнопка "Стереть" )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.DeleteObjects;
            PictureBox1.Cursor = Cursors.Hand;
        }
        /// <summary>
        /// Удалить все объекты ( Кнопка "Очистить всё" )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ControlDraw.Objects_ClearAll(PictureBox1, PictureBox2);
            PictureBox1.Cursor = Cursors.Default;
        }
        /// <summary>
        /// Удаление выбранных объектов ( Кнопка "Удалить выбранное")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.DeleteSelectObjects;
            ControlDraw.ObjectsSelected_Delete(PictureBox1, PictureBox2);
            PictureBox1.Cursor = Cursors.Default;
        }
        /// <summary>
        /// Обработчик события нажатия некоторых кнопок ( Кнопка "ESC") 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.None;
                PictureBox1.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 3D точка ( Кнопка 3D точка)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetPoint3D;
            PictureBox1.Cursor = Cursors.Cross;
        }
        /// <summary>
        /// Отрисовка горизонтальной проекции 3D точки ( кнопка "Проекция точки на Pi1")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetPointPlan1X0Y;
            PictureBox1.Cursor = Cursors.Cross;
        }
        /// <summary>
        /// Отрисовка фронтальной проекции 3D точки ( кнопка "Проекция точки Pi2" )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetPointPlan2X0Z;
            PictureBox1.Cursor = Cursors.Cross;
        }
        /// <summary>
        /// Отрисовка профильной проекции 3D точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetPointPlan3Y0Z;
            PictureBox1.Cursor = Cursors.Cross;
        }
        /// <summary>
        /// Сгенерировать 3D точку по заранее изображенным проекциям ( кнопка "Сгенерировать точку" )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            //1. Выбрать одну из ранее изображенных проекций
            //2. При выборе передавать первую точку в PointOfPlan1_X0Y_Var или PointOfPlan2_X0Z_Var или PointOfPlan3_Y0Z_Var проверив ее тип в Create_Point3DByDrawProection 
            //3. Проверить выбираемые последующие проекции путем отработки DoEvents_Point3D (построить новый метод путем переработки DoEvents_Point3D) и Create_Point3DByDrawProection
            //4. Перерисовать (обрубить) линии связи при правильной генерации 3D точки 
            PropertyPoint3D.FlagPoint_Point3D = true;
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetPoint3DByPointOfPlan;
            PictureBox1.Cursor = Cursors.Hand;
            ControlDraw.UserMouseClick = null;
        }
        /// <summary>
        /// Цикл отслеживания событий для возможности указания нескольких проекций для формирования 3D точки
        /// </summary>
        private void DoEvents_Point3D()
        {
            do {
                if (PropertyPoint3D.FlagPoint3D_CreateStop) break;

                if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetPoint3D) {
                } else break;
                if (PropertyPoint3D.FlagPoint_Point3D) {
                    DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
                    ControlDraw.Objects_DrawAndAdd(PictureBox1);
                    if (PropertyPoint3D.FlagPoint_Point3D) { } else {
                        if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                            DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
                            ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                        } else {
                            PropertyPoint3D.Draw_ProectionsTemp(DrawObjectsToPictureBox.GraphicsActive);
                        }
                        PictureBox1.Image = (Bitmap)DrawObjectsToPictureBox.BitmapActive.Clone();
                        PictureBox1.Refresh();
                    }
                    PropertyPoint3D.FlagPoint_Point3D = false;
                }
                Application.DoEvents();
            }
            while (true);
            PropertyPoint3D.FlagPoint3D_CreateStop = false;
            if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
                ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
            }
            PictureBox1.Image = (Bitmap)DrawObjectsToPictureBox.BitmapActive.Clone();
            PictureBox1.Refresh();
        }
        private void DoEvents_Line3D()
        {
            do {
                if (PropertyLine3D.FlagPoint3DCreateStop) break;

                if (DrawObjectsToPictureBox.Tool_DrawObject == DrawObjectsToPictureBox.Tools_DrawObjects.SetLine3D) {
                } else break;
                if (PropertyLine3D.FlagLine3D) {
                    DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
                    ControlDraw.Objects_DrawAndAdd(PictureBox1);
                    if (PropertyLine3D.FlagLine3D) { } else {
                        if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                            DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
                            ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                        } else {
                            PropertyLine3D.DrawTempPointsProjections(DrawObjectsToPictureBox.GraphicsActive);
                        }
                        PictureBox1.Image = (Bitmap)DrawObjectsToPictureBox.BitmapActive.Clone();
                        PictureBox1.Refresh();
                    }
                    PropertyLine3D.FlagLine3D = false;
                }
                Application.DoEvents();
            }
            while (true);
            PropertyLine3D.FlagPoint3DCreateStop = false;
            if (ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag) {
                DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
                ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
            }
            PictureBox1.Image = (Bitmap)DrawObjectsToPictureBox.BitmapActive.Clone();
            PictureBox1.Refresh();
        }
        /// <summary>
        /// Отрисовка простой линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetLine;
            PictureBox1.Cursor = Cursors.Cross;
        }

        private void toolStripStatusLabel6_StatusLinkLine_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel6_StatusLinkLine.BorderStyle == Border3DStyle.RaisedInner) {
                toolStripStatusLabel6_StatusLinkLine.BorderStyle = Border3DStyle.SunkenOuter;
                ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag = true;
                ControlDraw.LinkLine_Var.LinkLineToGrpahics_Add(DrawObjectsToPictureBox.GraphicsActive);
                DrawObjectsToPictureBox.GraphicsBack_Add(PictureBox1, PictureBox2);
            } else {
                toolStripStatusLabel6_StatusLinkLine.BorderStyle = Border3DStyle.RaisedInner;
                ControlDraw.LinkLine_Var.ShowLinkLine_XYZ_Flag = false;
                DrawObjectsToPictureBox.GraphicsBack_Clear(PictureBox1, PictureBox2);
            }
        }
        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetLinePlan1X0Y;
            PictureBox1.Cursor = Cursors.Cross;
        }
        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetLinePlan2X0Z;
            PictureBox1.Cursor = Cursors.Cross;
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetLinePlan3Y0Z;
            PictureBox1.Cursor = Cursors.Cross;
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            DrawObjectsToPictureBox.Tool_DrawObject = DrawObjectsToPictureBox.Tools_DrawObjects.SetLine3D;
            PictureBox1.Cursor = Cursors.Cross;
        }
    }
}