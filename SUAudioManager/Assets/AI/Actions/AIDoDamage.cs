using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using RAIN.Representation;

[RAINAction]
public class AIDoDamage : RAINAction
{
	public Expression enemy;
	private Health enemyHealth = null;
	private GameObject enemyObject = null;
	float lasthittime = 1f;
    public AIDoDamage()
    {
        actionName = "AIDoDamage";
    }

    public override void Start(AI ai)
    {
		enemyObject = enemy.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<GameObject>();
		if (enemyObject != null)
			enemyHealth = enemyObject.GetComponent<Health>();
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
		if(Time.time - lasthittime < .5f || enemyHealth == null)
		{
			return ActionResult.FAILURE;
		}

		enemyHealth.Damage(3f);
		lasthittime = Time.time;
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}