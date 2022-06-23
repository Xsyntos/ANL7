using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Steven Rietberg 1008478
 * Yannick de Vreede 1009289
 */
namespace ANL7
{
    abstract class ColorFactory
    {
        public ColorFactory() { }
        public abstract Color BlendColor();
    }

    abstract class Color
    {
        public string ColorValue;

        public Color(string color)
        {
            ColorValue = color;
        }
    }

    class Blue : Color { public Blue() : base("Blue") { } }
    class Brown : Color { public Brown() : base("Brown") { } }
    class Red : Color { public Red() : base("Red") { } }
    class White : Color { public White() : base("White") { } }
    class Green : Color { public Green() : base("Green") { } }
    class Neutral : Color { public Neutral() : base("") { } }


    class BlueFactory : ColorFactory { public override Color BlendColor() { return new Blue(); }}
    class BrownFactory : ColorFactory { public override Color BlendColor() { return new Brown(); } }
    class RedFactory : ColorFactory { public override Color BlendColor() { return new Red(); } }
    class WhiteFactory : ColorFactory { public override Color BlendColor() { return new White(); } }
    class GreenFactory : ColorFactory { public override Color BlendColor() { return new Green(); } }
    class NeutralFactory : ColorFactory { public override Color BlendColor() { return new Neutral(); } }




}
