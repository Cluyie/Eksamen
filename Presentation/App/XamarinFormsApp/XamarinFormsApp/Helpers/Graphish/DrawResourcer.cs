using System;
using System.Collections.Generic;
using Models.Interfaces;
using SkiaSharp;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.Helpers.Graphish
{
    public class DrawResourcer
    {
        private readonly SKPaint Box = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            StrokeCap = SKStrokeCap.Round
        };

        private readonly SKPaint Outlinje = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.Blue
        };

        private readonly SKPaint Text = new SKPaint
        {
            TextSize = 24,
            TextAlign = SKTextAlign.Left,
            Color = SKColors.Black
        };

        private readonly int xRounding = 10;
        private readonly int yRounding = 10;

        public void DrawDay(SKCanvas canvas, int width, int height, List<IAvailableTime> AvailabelToDay,
            List<IReservation<ReserveTime>> ReservatToDay, Guid GuidForOwnerResv)
        {
            var PixPrMin = height / (24 * 60f);
            Box.Color = SKColors.LightGray;
            foreach (var availableTime in AvailabelToDay)
            {
                var theDay = new DateTime(availableTime.From.Year, availableTime.From.Month, availableTime.From.Day, 0,
                    0, 0);
                var yTop = (float) availableTime.From.TimeOfDay.TotalMinutes * PixPrMin;
                var yBotton = (float) availableTime.To.TimeOfDay.TotalMinutes * PixPrMin;
                canvas.DrawRect(new SKRect(0, yTop, width, yBotton), Box);
                if (yBotton - yTop > 25)
                    canvas.DrawText(availableTime.From.TimeOfDay.ToString("hh':'mm"), 5, yTop + 24, Text);
                canvas.DrawText(availableTime.To.TimeOfDay.ToString("hh':'mm"), 5, yBotton + 24, Text);
            }

            canvas.Save();
            foreach (var resrvation in ReservatToDay)
            {
                if (resrvation.UserId.Equals(GuidForOwnerResv))
                    Box.Color = SKColors.CadetBlue;
                else
                    Box.Color = SKColors.LightGray;

                IReserveTime reserveTime = resrvation.Timeslot;
                var yTop = (float) reserveTime.FromDate.TimeOfDay.TotalMinutes * PixPrMin;
                var yBottum = (float) reserveTime.ToDate.TimeOfDay.TotalMinutes * PixPrMin;

                var Boxen = new SKRect(0, yTop, width - 1, yBottum);
                canvas.DrawRoundRect(Boxen, xRounding, yRounding, Box);
                canvas.DrawRoundRect(Boxen, xRounding, yRounding, Outlinje);
                canvas.DrawRoundRect(Boxen.Left + 1, Boxen.Top + 1, Boxen.Width - 2, Boxen.Height - 2, xRounding,
                    yRounding, Outlinje);
                canvas.DrawRoundRect(Boxen.Left + 2, Boxen.Top + 2, Boxen.Width - 4, Boxen.Height - 4, xRounding,
                    yRounding, Outlinje);
                if (Boxen.Height > 25)
                    canvas.DrawText(reserveTime.FromDate.TimeOfDay.ToString("hh':'mm"), Boxen.Left + 5, Boxen.Top + 24,
                        Text);
                canvas.DrawText(reserveTime.ToDate.TimeOfDay.ToString("hh':'mm"), Boxen.Left + 5, Boxen.Bottom + 24,
                    Text);
            }

            canvas.Restore();
        }
    }
}