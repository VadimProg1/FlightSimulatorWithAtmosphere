using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulation
{
    class BusinessModel
    {
        decimal t, x = 0, y = 0, v0, cosA, sinA, S, m, k, vx, vy;
        const decimal g = 9.81M, C = 0.15M, rho = 1.29M, dt = 0.1M;
        public void GetStarted(decimal height, decimal speed, decimal angle,
            decimal weight, decimal size)
        {
            t = 0;
            x = 0;
            y = height;
            v0 = speed;
            double a = (double)angle * Math.PI / 180;
            cosA = (decimal)Math.Cos(a);
            sinA = (decimal)Math.Sin(a);
            S = size;
            m = weight;
            k = 0.5M * C * rho * S / m;
            vx = v0 * cosA;
            vy = v0 * sinA;     
        }

        public void NextTick()
        {
            t += dt;
            decimal v = (decimal)Math.Sqrt((double)(vx * vx + vy * vy));
            vx = vx - k * vx * v * dt;
            vy = vy - (g + k * vy * v) * dt;
            x = x + vx * dt;
            y = y + vy * dt;
        }

        public decimal GetX()
        {
            return x;
        }

        public decimal GetY()
        {
            return y;
        }
    }
}
