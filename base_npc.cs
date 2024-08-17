using Godot;
using System;
using System.Diagnostics.Tracing;


public partial class base_npc : StaticBody2D
{
	[Export]
	public float speed = 1;
	float scale = 1;
	Vector2 prev_pos;
	int facing;
	int food;

	[Export]
	int base_food = 5;
	public AnimationController animation_controller;
	Vector2 Velocity;
	public Vector2 target;
	
	public void setSize()
	{
		getFacing();
		// GD.Print($"{Name}:Facing:{facing}");
		// GD.Print($"{Name}: Scale: {scale}");
		scale = (float)food / base_food;
		Vector2 one_vector = new Vector2(1f,1f);
		Scale = one_vector * scale;
		
	}

	public virtual void Eat(mouse player)
	{
		if(food < base_food)
		{
			food++;
		}
		player.getEaten();
		animation_controller.animation = "eat";
	}

	public virtual void setTarget()
	{
		
	}

	public void getFacing()
	{
		Vector2 difference = GlobalPosition - prev_pos;
		// GD.Print($"{Name} difference: {difference}");
		if(difference.X > 0f)
		{
			facing = 1;
		}
		else
		{
			facing = -1;
		}

		prev_pos = GlobalPosition;
	}


	public void die()
	{
		if (food <= 0)
		{
			QueueFree();
		}
	}

	public void getEaten()
	{
		GD.Print("Getting Eaten");
		GD.Print($"food: {food}, scale: {Scale}");
		food--;
	}

	public virtual void move(double delta)
	{
		// GD.Print($"{Name}: Moving");
		Vector2 direction = getDirection();
		Velocity = speed * direction;
		Translate(Velocity * (float)delta);
		animation_controller.direction = direction;
	}

	public virtual Vector2 getDirection()
	{
		//direction will come from ai behavior
		// target = Position;
		
		float distance = (target - Position).Length();
		if(distance > 16f)
		{
			Vector2 direction = (target - Position).Normalized();
			return direction;
		}
		else
		{
			Vector2 direction = new Vector2(0f, 0f);
			return direction;
		}
		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		prev_pos = GlobalPosition;
		food = base_food;
		animation_controller = GetNode<AnimationController>("AnimationController");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		setSize();
		move(delta);
		die();

	}

}
