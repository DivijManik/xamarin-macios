// Copyright 2011 Xamarin Inc. All rights reserved

#if !__WATCHOS__ && !MONOMAC

using System;
using System.Drawing;
#if XAMCORE_2_0
using Foundation;
using UIKit;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#endif
using NUnit.Framework;

#if XAMCORE_2_0
using RectangleF=CoreGraphics.CGRect;
using SizeF=CoreGraphics.CGSize;
using PointF=CoreGraphics.CGPoint;
#else
using nfloat=global::System.Single;
using nint=global::System.Int32;
using nuint=global::System.UInt32;
#endif

namespace MonoTouchFixtures.UIKit {
	
	[TestFixture]
	[Preserve (AllMembers = true)]
	public class ViewTest {
		
		[Test]
		public void InitWithFrame ()
		{
			RectangleF frame = new RectangleF (10, 10, 100, 100);
			using (UIView v = new UIView (frame)) {
				Assert.That (v.Frame, Is.EqualTo (frame), "Frame");
			}
		}

		[Test]
		public void HitTest_Null ()
		{
			RectangleF frame = new RectangleF (10, 10, 100, 100);
			using (UIView v = new UIView (frame)) {
				UIView result = v.HitTest (new PointF (-10, -10), null);
				Assert.Null (result, "outside");
				result = v.HitTest (new PointF (50, 50), null);
				Assert.That (result.Handle, Is.EqualTo (v.Handle), "inside");
			}
		}

		[Test]
		public void PointInside_Null ()
		{
			RectangleF frame = new RectangleF (10, 10, 100, 100);
			using (UIView v = new UIView (frame)) {
				Assert.False (v.PointInside (new PointF (-10, -10), null), "outside");
				Assert.True (v.PointInside (new PointF (50, 50), null), "inside");
			}
		}

		[Test]
		public void SizeThatFits ()
		{
			// same as LinkerTest in 'linksdk' project - but won't be linked here (for simulator)
			SizeF empty = SizeF.Empty;
			using (UIView v = new UIView ()) {
				Assert.True (v.SizeThatFits (empty).IsEmpty, "Empty");
			}
		}
		
		[Test]
		public void Convert_Null ()
		{
			using (UIView v = new UIView ()) {
				Assert.That (v.ConvertPointFromView (PointF.Empty, null), Is.EqualTo (PointF.Empty), "ConvertPointFromView");
				Assert.That (v.ConvertPointToView (PointF.Empty, null), Is.EqualTo (PointF.Empty), "ConvertPointToView");
				Assert.That (v.ConvertRectFromView (RectangleF.Empty, null), Is.EqualTo (RectangleF.Empty), "ConvertRectFromView");
				Assert.That (v.ConvertRectToView (RectangleF.Empty, null), Is.EqualTo (RectangleF.Empty), "ConvertRectToView");
			}
		}
		
		// Apple does not allow NULL on 'animations' parameters used in animate* and transition* selectors
		
		void Completion ()
		{
		}
		
		void CompletionHandler (bool finished)
		{
		}
		
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Animate_Null_a1 ()
		{
			UIView.Animate (1.0, null);
		}
		
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Animate_Null_a2 ()
		{
			UIView.Animate (1.0, null, Completion);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Animate_Null_a3 ()
		{
			UIView.Animate (1.0, 2.0, UIViewAnimationOptions.Autoreverse, null, Completion);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void AnimateNotify_Null_a1 ()
		{
			UIView.AnimateNotify (1.0, null, CompletionHandler);
		}
		
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void AnimateNotify_Null_a2 ()
		{
			UIView.AnimateNotify (1.0, 2.0, UIViewAnimationOptions.Autoreverse, null, CompletionHandler);
		}
		
		[Test]
		public void Transition_Null_a1 ()
		{
			using (UIView v = new UIView ()) {
				UIView.Transition (v, 1.0, UIViewAnimationOptions.AllowAnimatedContent, null, Completion);
			}
		}

		[Test]
		public void TransitionNotify_Null_a1 ()
		{
			using (UIView v = new UIView ()) {
				UIView.TransitionNotify (v, 1.0, UIViewAnimationOptions.AllowAnimatedContent, null, CompletionHandler);
			}
		}
		
		// Apple allows NULL on 'completion' parameters used in animate* and transition* selectors

		void Animations ()
		{
		}
		
		[Test]
		public void Animate_Null_c1 ()
		{
			UIView.Animate (1.0, Animations, null);
		}

		[Test]
		public void Animate_Null_c2 ()
		{
			UIView.Animate (1.0, 2.0, UIViewAnimationOptions.Autoreverse, Animations, null);
		}

		[Test]
		public void AnimateNotify_Null_c1 ()
		{
			UIView.AnimateNotify (1.0, Animations, null);
		}
		
		[Test]
		public void AnimateNotify_Null_c2 ()
		{
			UIView.AnimateNotify (1.0, 2.0, UIViewAnimationOptions.Autoreverse, Animations, null);
		}
		
		[Test]
		public void Transition_Null_c1 ()
		{
			using (UIView v = new UIView ()) {
				UIView.Transition (v, 1.0, UIViewAnimationOptions.AllowAnimatedContent, Animations, null);
			}
		}

		[Test]
		public void Transition_Null_c2 ()
		{
			using (UIView v = new UIView ()) {
				UIView.Transition (v, v, 1.0, UIViewAnimationOptions.AllowAnimatedContent, null);
			}
		}

		[Test]
		public void TransitionNotify_Null_c1 ()
		{
			using (UIView v = new UIView ()) {
				UIView.TransitionNotify (v, 1.0, UIViewAnimationOptions.AllowAnimatedContent, Animations, null);
			}
		}

		[Test]
		public void TransitionNotify_Null_c2 ()
		{
			using (UIView v = new UIView ()) {
				UIView.TransitionNotify (v, v, 1.0, UIViewAnimationOptions.AllowAnimatedContent, null);
			}
		}
		
		[Test]
		public void BackgroundColorTest ()
		{
			using (var color = UIColor.FromRGB (5, 6, 7)) {
				using (var view = new ViewWithCustomBackgroundColor ()) {
					Messaging.void_objc_msgSend_IntPtr (view.Handle, new Selector ("setBackgroundColor:").Handle, color.Handle);
				}
			}
		}
		
		[Register ("ViewWithCustomBackgroundColor")]
		public class ViewWithCustomBackgroundColor : UIView
		{
			public override UIColor BackgroundColor {
				get { return base.BackgroundColor; }
				set { base.BackgroundColor = value; }
			}
		}

		[Test]
		public void TraitTest ()
		{
			using (var view = new UIView ()) {
				Assert.AreEqual (UIAccessibilityTrait.None, view.AccessibilityTraits, "a");
				view.AccessibilityTraits = UIAccessibilityTrait.None;
				Assert.AreEqual (UIAccessibilityTrait.None, view.AccessibilityTraits, "b");
				view.AccessibilityTraits = UIAccessibilityTrait.Adjustable;
				Assert.AreEqual (UIAccessibilityTrait.Adjustable, view.AccessibilityTraits, "c");
				view.AccessibilityTraits = UIAccessibilityTrait.Adjustable | UIAccessibilityTrait.Button;
				Assert.AreEqual (UIAccessibilityTrait.Adjustable | UIAccessibilityTrait.Button, view.AccessibilityTraits, "e");
			}
		}
		
		[Test]
		public void TraitMatch ()
		{
			Assert.AreEqual ((int) UIAccessibilityTrait.Adjustable, UIView.TraitAdjustable, "Adjustable");
			Assert.AreEqual ((int) UIAccessibilityTrait.AllowsDirectInteraction, UIView.TraitAllowsDirectInteraction, "AllowsDirectInteraction");
			Assert.AreEqual ((int) UIAccessibilityTrait.Button, UIView.TraitButton, "Button");
			Assert.AreEqual ((int) UIAccessibilityTrait.CausesPageTurn, UIView.TraitCausesPageTurn, "CausesPageTurn");
			Assert.AreEqual ((int) UIAccessibilityTrait.Image, UIView.TraitImage, "Image");
			Assert.AreEqual ((int) UIAccessibilityTrait.KeyboardKey, UIView.TraitKeyboardKey, "KeyboardKey");
			Assert.AreEqual ((int) UIAccessibilityTrait.Link, UIView.TraitLink, "Link");
			Assert.AreEqual ((int) UIAccessibilityTrait.None, UIView.TraitNone, "None");
			Assert.AreEqual ((int) UIAccessibilityTrait.NotEnabled, UIView.TraitNotEnabled, "NotEnabled");
			Assert.AreEqual ((int) UIAccessibilityTrait.PlaysSound, UIView.TraitPlaysSound, "PlaysSound");
			Assert.AreEqual ((int) UIAccessibilityTrait.SearchField, UIView.TraitSearchField, "SearchField");
			Assert.AreEqual ((int) UIAccessibilityTrait.Selected, UIView.TraitSelected, "Selected");
			Assert.AreEqual ((int) UIAccessibilityTrait.StartsMediaSession, UIView.TraitStartsMediaSession, "StartsMediaSession");
			Assert.AreEqual ((int) UIAccessibilityTrait.StaticText, UIView.TraitStaticText, "StaticText");
			Assert.AreEqual ((int) UIAccessibilityTrait.SummaryElement, UIView.TraitSummaryElement, "SummaryElement");
			Assert.AreEqual ((int) UIAccessibilityTrait.UpdatesFrequently, UIView.TraitUpdatesFrequently, "UpdatesFrequently");

			// some [Field] won't return a valid value before iOS 6.0
			if (TestRuntime.CheckSystemAndSDKVersion (6,0))
				Assert.AreEqual ((int) UIAccessibilityTrait.Header, UIView.TraitHeader, "Header");
		}

		[Test]
		public void Subviews ()
		{
			using (var v = new UIView ()) {
				Assert.That (v.Subviews, Is.Not.Null);

				// even if null we want to ensure we can use UIView.GetEnumarator to iterate subviews
				int n = 0;
				foreach (var sv in v)
					Assert.NotNull (sv, n++.ToString ());
			}
		}

		[Test]
		public void TintColor ()
		{
			if (!TestRuntime.CheckSystemAndSDKVersion (7, 0))
				Assert.Ignore ("requires iOS7+");

			using (var v = new UIView ()) {
				var tc = v.TintColor;
				Assert.NotNull (tc, "TintColor-1");
				v.TintColor = UIColor.Red;
				v.TintColor = null;
				// setting to null returns to default (i.e. not the last non-null value)
				Assert.NotNull (v.TintColor, "TintColor-2");
			}
		}

		[Test]
		public void Equality ()
		{
			using (var v1 = new UIView ())
			using (var v2 = new UIView ()) {
				// two basic/init'ed instances differ only by their handles
				Assert.That (v1.Handle, Is.Not.EqualTo (v2.Handle), "Handle");
				// and that's enough to make them totally different (natively in objc for both `hash` and `isEqual:`)
				Assert.That (v1.GetHashCode (), Is.Not.EqualTo (v2.GetHashCode ()), "GetHashCode");
				Assert.False (v1.Equals (v2.Handle), "Equals");
			}
		}
	}
}

#endif // !__WATCHOS__
