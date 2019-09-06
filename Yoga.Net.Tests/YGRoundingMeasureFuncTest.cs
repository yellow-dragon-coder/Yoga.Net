using NUnit.Framework;
using static Yoga.Net.YogaGlobal;


namespace Yoga.Net.Tests
{
    [TestFixture]
    public class YGRoundingMeasureFuncTest
    {
        static MeasureFunc _measureFloor = (
            YogaNode node,
            float width,
            MeasureMode widthMode,
            float height,
            MeasureMode heightMode,
            object context) =>
        {
            return new YogaSize(10.2f, 10.2f);
        };

        static MeasureFunc _measureCeil = (
            YogaNode node,
            float width,
            MeasureMode widthMode,
            float height,
            MeasureMode heightMode,
            object context) =>
        {
            return new YogaSize(10.5f, 10.5f);
        };

        static MeasureFunc _measureFractial = (
            YogaNode node,
            float width,
            MeasureMode widthMode,
            float height,
            MeasureMode heightMode, object context) =>
        {
            return new YogaSize(0.5f, 0.5f);
        };

        [Test]
        public void rounding_feature_with_custom_measure_func_floor()
        {
            YogaConfig config = YGConfigNew();
            YogaNode root = YGNodeNewWithConfig(config);

            YogaNode root_child0 = YGNodeNewWithConfig(config);
            root_child0.SetMeasureFunc(_measureFloor);
            YGNodeInsertChild(root, root_child0, 0);

            YGConfigSetPointScaleFactor(config, 0.0f);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.RTL);

            Assert.AreEqual(10.2f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10.2f, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 1.0f);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);

            Assert.AreEqual(11f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(11f, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 2.0f);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.RTL);

            Assert.AreEqual(10.5f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10.5f, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 4.0f);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);

            Assert.AreEqual(10.25f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10.25f, YGNodeLayoutGetHeight(root_child0));

            YGConfigSetPointScaleFactor(config, 1.0f / 3.0f);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.RTL);

            Assert.AreEqual(12.0f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(12.0f, YGNodeLayoutGetHeight(root_child0));
        }

        [Test]
        public void rounding_feature_with_custom_measure_func_ceil()
        {
            YogaConfig config = YGConfigNew();
            YogaNode root = YGNodeNewWithConfig(config);

            YogaNode root_child0 = YGNodeNewWithConfig(config);
            root_child0.SetMeasureFunc(_measureCeil);
            YGNodeInsertChild(root, root_child0, 0);
            YGConfigSetPointScaleFactor(config, 1.0f);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);

            Assert.AreEqual(11, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(11, YGNodeLayoutGetHeight(root_child0));
        }

        [Test]
        public void rounding_feature_with_custom_measure_and_fractial_matching_scale()
        {
            YogaConfig config = YGConfigNew();
            YogaNode root = YGNodeNewWithConfig(config);

            YogaNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetPosition(root_child0, Edge.Left, 73.625f);
            root_child0.SetMeasureFunc(_measureFractial);
            YGNodeInsertChild(root, root_child0, 0);

            YGConfigSetPointScaleFactor(config, 2.0f);

            YGNodeCalculateLayout(root, YogaValue.YGUndefined, YogaValue.YGUndefined, Direction.LTR);

            Assert.AreEqual(0.5, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0.5, YGNodeLayoutGetHeight(root_child0));
            Assert.AreEqual(73.5, YGNodeLayoutGetLeft(root_child0));
        }
    }
}
