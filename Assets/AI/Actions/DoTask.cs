using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class DoTask : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		//interacts with the target object
		GameObject task_object = ai.WorkingMemory.GetItem<GameObject>("task_object");
		foreach(MonoBehaviour script in task_object.GetComponents<MonoBehaviour>())
		{
			IAiInteractable aiScript = script as IAiInteractable;
			if(aiScript != null)
			{
				aiScript.AiInteract(ai.Body, ai.WorkingMemory.GetItem<string>("type"));
			}
		}

		
		ai.WorkingMemory.SetItem<bool>("has_task", false);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}