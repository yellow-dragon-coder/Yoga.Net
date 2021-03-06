using NUnit.Framework;
using static Yoga.Net.YGGlobal;



namespace Yoga.Net.Tests
{
    [TestFixture]
    public class YGRelayoutTest
    {

        [Test] public void dont_cache_computed_flex_basis_between_layouts() {
            YGConfig config = YGConfigNew();
            YGConfigSetExperimentalFeatureEnabled(config, YGExperimentalFeature.WebFlexBasis, true);

            YGNode root = YGNodeNewWithConfig(config);
            YGNodeStyleSetHeightPercent(root, 100);
            YGNodeStyleSetWidthPercent(root, 100);

            YGNode root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexBasisPercent(root_child0, 100);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, 100, YGValue.YGUndefined, YGDirection.LTR);
            YGNodeCalculateLayout(root, 100, 100, YGDirection.LTR);

            Assert.AreEqual(100, YGNodeLayoutGetHeight(root_child0));

            

            
        }

        [Test] public void recalculate_resolvedDimonsion_onchange() {
            YGNode root = YGNodeNew();

            YGNode root_child0 = YGNodeNew();
            YGNodeStyleSetMinHeight(root_child0, 10);
            YGNodeStyleSetMaxHeight(root_child0, 10);
            YGNodeInsertChild(root, root_child0, 0);

            YGNodeCalculateLayout(root, YGValue.YGUndefined, YGValue.YGUndefined, YGDirection.LTR);
            Assert.AreEqual(10, YGNodeLayoutGetHeight(root_child0));

            YGNodeStyleSetMinHeight(root_child0, YGValue.YGUndefined);
            YGNodeCalculateLayout(root, YGValue.YGUndefined, YGValue.YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetHeight(root_child0));

            
        }
    }
}