using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
            // так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
            var r1Right = r1.Left + r1.Width;
            var r2Right = r2.Left + r2.Width;
            var r1Floor = r1.Top + r1.Height;
            var r2Floor = r2.Top + r2.Height;
            var firstAnswer = ((r1.Left <= r2Right && r2Right <= r1Right) || (r1.Left <= r2.Left && r2.Left <= r1Right)) &&
                ((r1.Top <= r2.Top && r2.Top <= r1Floor) || (r2.Top <= r1.Top && r1.Top <= r2Floor));
            var secondAnswer = ((r1.Top <= r2Floor && r2Floor <= r1Floor) || (r1.Top <= r2.Top && r2.Top <= r1Floor)) && 
                ((r1Right <= r2Right && r2Right <= r1.Left) || (r2Right <= r1Right && r1Right <= r2.Left));
            bool thirdAnswer;
            var result = IndexOfInnerRectangle(r1, r2);
            if (result == 1 || result == 0) thirdAnswer = true;
            else thirdAnswer = false;
            return firstAnswer || secondAnswer || thirdAnswer;
		}

        // Площадь пересечения прямоугольников
        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var r1Right = r1.Left + r1.Width;
            var r2Right = r2.Left + r2.Width;
            var r1Floor = r1.Top + r1.Height;
            var r2Floor = r2.Top + r2.Height;
            int dx, dy, answer;

            if (r1Right >= r2Right && r1.Left <= r2.Left) dx = r2Right - r2.Left;
            else if (r1Right <= r2Right && r2.Left <= r1Right && r2.Left >= r1.Left) dx = r1Right - r2.Left;
            else if (r2.Left <= r1.Left && r2Right >= r1.Left && r2Right <= r1.Left) dx = r2Right - r1.Left;
            else if (r1Right >= r2Right && r2.Left >= r1Right && r2.Left <= r1.Left) dx = -r1Right + r2.Left;
            else if (r2.Left >= r1.Left && r2Right <= r1.Left && r2Right >= r1.Left) dx = -r2Right + r1.Left;
            else dx = 0;

            if (r1.Top <= r2.Top && r1Floor >= r2Floor) dy = r2Floor - r2.Top;
            else if (r1.Top <= r2.Top && r2.Top <= r1Floor && r2Floor >= r1Floor) dy = r1Floor - r2.Top;
            else if (r1Floor <= r2Floor && r2Floor <= r1.Top && r1.Top <= r2.Top) dy = r2Floor - r1.Top;
            else if (r1.Top <= r2.Top && r2.Top >= r1Floor && r2Floor <= r1Floor) dy = -r1Floor + r2.Top;
            else if (r1Floor >= r2Floor && r2Floor >= r1.Top && r1.Top >= r2.Top) dy = -r2Floor + r1.Top;
            else dy = 0;

            //if (r1.Top <= r2.Top && r1Floor >= r2Floor) dy = r2Floor - r2.Top;
            //else if (r1.Top >= r2.Top && r2Floor >= r1.Top && r2Floor <= r1Floor) dy = r2Floor - r1.Top;
            //else if (r2Floor >= r1Floor && r2.Top <= r1Floor && r2.Top <= r1.Top) dy = r2.Top - r1Floor;
            //else dy = 0;

            answer = dx * dy;
            if (answer < 0)
                answer = -answer;
            //answer = dy;
            return answer;
        }

        // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
        // Иначе вернуть -1
        // Если прямоугольники совпадают, можно вернуть номер любого из них.
        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
            var r1Right = r1.Left + r1.Width;
            var r2Right = r2.Left + r2.Width;
            var r1Floor = r1.Top + r1.Height;
            var r2Floor = r2.Top + r2.Height;
            int answer;
            if (r1.Top >= r2.Top && r1.Left >= r2.Left && r1Floor <= r2Floor && r1Right <= r2Right) answer = 0;
            else if (r2.Top >= r1.Top && r2.Left >= r1.Left && r2Floor <= r1Floor && r2Right <= r1Right) answer = 1;
            else answer = -1;
            return answer;
		}
	}
}