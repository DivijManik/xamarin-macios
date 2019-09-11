//
// ARSkeleton3D.cs: Nicer code for ARSkeleton3D
//
// Authors:
//	Vincent Dondain  <vidondai@microsoft.com>
//
// Copyright 2019 Microsoft Inc. All rights reserved.
//

#if XAMCORE_2_0

using System;
using System.Runtime.InteropServices;
using Matrix4 = global::OpenTK.NMatrix4;

namespace ARKit {
	public partial class ARSkeleton3D {

		public unsafe Matrix4 [] JointModelTransforms {
			get {
				var count = (int)JointCount;
				var rv = new Matrix4 [count];
				var ptr = (Matrix4 *) RawJointModelTransforms;
				for (int i = 0; i < count; i++)
					rv [i] = *ptr++;
				return rv;
			}
		}

		public unsafe Matrix4 [] JointLocalTransforms {
			get {
				var count = (int)JointCount;
				var rv = new Matrix4 [count];
				var ptr = (Matrix4 *) RawJointLocalTransforms;
				for (int i = 0; i < count; i++)
					rv [i] = *ptr++;
				return rv;
			}
		}
	}
}

#endif