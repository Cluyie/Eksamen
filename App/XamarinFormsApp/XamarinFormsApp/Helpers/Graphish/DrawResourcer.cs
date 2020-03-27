using Models.Interfaces;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.Helpers.Graphish
{
    public class DrawResourcer
    {
        private SKPaint Box = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            StrokeCap = SKStrokeCap.Round
        };

        private SKPaint Outlinje = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.Blue
        };

        private SKPaint Text = new SKPaint
        {
            TextSize = 24,
            TextAlign = SKTextAlign.Left,
            Color = SKColors.Black
        };

        private int xRounding = 10;
        private int yRounding = 10;

        public void DrawDay(SKCanvas canvas, int width, int height, List<IAvailableTime> AvailabelToDay, List<IReservation<ReserveTime>> ReservatToDay, Guid GuidForOwnerResv)
        {
            float PixPrMin = height / (24 * 60f);
            Box.Color = SKColors.White;
            foreach (IAvailableTime availableTime in AvailabelToDay)
            {
                DateTime theDay = new DateTime(availableTime.From.Year, availableTime.From.Month, availableTime.From.Day, 0, 0, 0);
                float yTop = (float)availableTime.From.TimeOfDay.TotalMinutes * PixPrMin;
                float yBotton = (float)availableTime.To.TimeOfDay.TotalMinutes * PixPrMin;
                canvas.DrawRect(new SKRect(0, yTop, width, yBotton), Box);
                if (yBotton - yTop > 25)
                {
                    canvas.DrawText(availableTime.From.TimeOfDay.ToString("hh':'mm"), 5, yTop + 24, Text);
                }
                canvas.DrawText(availableTime.To.TimeOfDay.ToString("hh':'mm"), 5, yBotton + 24, Text);
            }
            canvas.Save();
            foreach (IReservation<ReserveTime> resrvation in ReservatToDay)
            {
                if (resrvation.UserId.Equals(GuidForOwnerResv))
                {
                    Box.Color = SKColors.CadetBlue;
                }
                else
                {
                    Box.Color = SKColors.White;
                }

                IReserveTime reserveTime = resrvation.Timeslot;
                float yTop = (float)reserveTime.FromDate.TimeOfDay.TotalMinutes * PixPrMin;
                float yBottum = (float)reserveTime.ToDate.TimeOfDay.TotalMinutes * PixPrMin;

                SKRect Boxen = new SKRect(0, yTop, width - 1, yBottum);
                canvas.DrawRoundRect(Boxen, xRounding, yRounding, Box);
                canvas.DrawRoundRect(Boxen, xRounding, yRounding, Outlinje);
                canvas.DrawRoundRect(Boxen.Left + 1, Boxen.Top + 1, Boxen.Width - 2, Boxen.Height - 2, xRounding, yRounding, Outlinje);
                canvas.DrawRoundRect(Boxen.Left + 2, Boxen.Top + 2, Boxen.Width - 4, Boxen.Height - 4, xRounding, yRounding, Outlinje);
                if (Boxen.Height > 25)
                {
                    canvas.DrawText(reserveTime.FromDate.TimeOfDay.ToString("hh':'mm"), Boxen.Left + 5, Boxen.Top + 24, Text);
                }
                canvas.DrawText(reserveTime.ToDate.TimeOfDay.ToString("hh':'mm"), Boxen.Left + 5, Boxen.Bottom + 24, Text);
            }
            canvas.Restore();
        }
    }
}