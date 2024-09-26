using UnityEngine;

namespace Generics.Dynamics
{
	public class InverseKinematics : MonoBehaviour
	{
		public Core.Solvers solver;

		public Core.Chain rArm;

		public Core.Chain lArm;

		public Core.Chain rLeg;

		public Core.Chain lLeg;

		public Core.Chain[] otherChains;

		public Core.KinematicChain[] otherKChains;

		public RigReader rigReader;

		public Animator animator;

		private void OnEnable()
		{
			if (rigReader == null)
			{
				DetectRig();
			}
		}

		private void LateUpdate()
		{
			switch (solver)
			{
			case Core.Solvers.CyclicDescend:
				CyclicDescendSolver.Process(rLeg);
				CyclicDescendSolver.Process(lLeg);
				CyclicDescendSolver.Process(rArm);
				CyclicDescendSolver.Process(lArm);
				for (int j = 0; j < otherChains.Length; j++)
				{
					CyclicDescendSolver.Process(otherChains[j]);
				}
				break;
			case Core.Solvers.FastReach:
				for (int i = 0; i < otherChains.Length; i++)
				{
					FastReachSolver.Process(otherChains[i]);
				}
				FastReachSolver.Process(rLeg);
				FastReachSolver.Process(lLeg);
				FastReachSolver.Process(rArm);
				FastReachSolver.Process(lArm);
				break;
			}
			for (int k = 0; k < otherKChains.Length; k++)
			{
				ChainKinematicSolver.Process(otherKChains[k]);
			}
		}

		public void DetectRig()
		{
			if (!animator)
			{
				animator = GetComponent<Animator>();
			}
			rigReader = new RigReader(animator);
		}

		public void BuildRig()
		{
			rArm = rigReader.RightArmChain();
			lArm = rigReader.LeftArmChain();
			rLeg = rigReader.RightLegChain();
			lLeg = rigReader.LeftLegChain();
		}
	}
}
