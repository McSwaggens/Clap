using System;

namespace Clap
{
	public class GraphicalInterface
	{
		public string name;
		public Location location;
		public Size size;
		public bool focused = false;
		public bool visible = true;
		public Clap clap;

		public GraphicalInterface (Clap clap)
		{
			this.clap = clap;
		}

		public virtual void Update()
		{
		}

		public void Focus()
		{
			if (focused)
				return;
			focused = true;
			clap.FocusedComponent = this;
			OnFocused ();
		}

		public void Unfocus()
		{
			if (!focused)
				return;
			focused = false;
			clap.RemoveFocus (this);
			OnUnfocused ();
		}

		public virtual void OnUnfocused()
		{
		}

		public virtual void OnFocused()
		{
		}

		public void Format(Location location, Size size) {
			this.size = size;
			OnResized ();
			this.location = location;
			OnMoved ();
		}

		public void Move(Location location) {
			this.location = location;
			OnMoved();
		}

		public void Resize(Size size) {
			this.size = size;
			OnResized ();
		}

		public virtual void OnResized()
		{
		}

		public virtual void OnMoved()
		{
		}

		public void Dispose()
		{
			clap.RemoveComponent (this);
			OnDisposed ();
			Unfocus ();
			clap.Refresh ();
		}

		public virtual void OnDisposed()
		{
		}

		public void SelfUpdate() {
			clap.CurrentlyUpdating = this;
			Update();
		}
	}
}

