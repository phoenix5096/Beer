using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollingItemMenu : MonoBehaviour 
{
	//game object place holders where the sprites will be drawn.
	public List<GameObject> slots;
	public List<Sprite> spriteList;
	public List<System.Object> values;
	public int selectedIndex = 0;
	public int masterSlotIndex = 1;
	public int fps = 25;

	private bool ScrollingRight = false;
	private bool ScrollingLeft = false;
	private List<SpriteRenderer> slotRenderers = new List<SpriteRenderer> ();
	private List<Vector3> slotOriginalPositions = new List<Vector3> ();
	private List<Vector3> slotOriginalScales = new List<Vector3> ();

	//The distance between Sprites (from left to right)  
	//ex: slotDistances[0] = distance between slots[0] and slots[1]
	private List<Vector3> slotDistances = new List<Vector3> ();

	//The scaling difference between Sprites (from left to right)  
	//ex: slotDeltaScales[0] = scale difference between slots[0] and slots[1]
	private List<Vector3> slotDeltaScales = new List<Vector3> ();

	public System.Object getSelectedValue()
	{
		return values [selectedIndex];
	}

	// Use this for initialization
	void Start () 
	{
		//store all required information about the menu slots
		for (int i = 0; i < slots.Count; i++) 
		{
			slotRenderers.Add(slots[i].GetComponent<SpriteRenderer> ());
			slotOriginalPositions.Add (new Vector3 (slots [i].transform.position.x, slots [i].transform.position.y, slots [i].transform.position.z));
			slotOriginalScales.Add (new Vector3( slots[i].transform.localScale.x, slots[i].transform.localScale.y,  slots[i].transform.localScale.z));
		}

		//store all required information about the menu slots
		for (int i = 0; i < slots.Count-1; i++) 
		{
			slotDistances.Add (slotOriginalPositions[i+1] - slotOriginalPositions[i]);
			slotDeltaScales.Add (slotOriginalScales[i+1] - slotOriginalScales[i]);
		}
	}
	

	public void ScrollRight()
	{
		if (ScrollingRight || ScrollingLeft) 
		{
			//cannot scroll again while animating
			return; 
		}

		ScrollingRight = true;
	}

	public void ScrollLeft()
	{
		if (ScrollingRight || ScrollingLeft) 
		{
			//cannot scroll again while animating
			return; 
		}

		ScrollingLeft = true;
	}

	public void OnGUI()
	{
		//Simple rules to follow:
		//-Must always have at least 3 slots (2 of them off screen and 1 on screen)
		//-Must always have at least 1 sprite
		//-The master slot must not be the first or last item (they are reserved for off-screen buffering)
		if (slots.Count < 3 || spriteList.Count < 1 || masterSlotIndex < 1 || masterSlotIndex > slots.Count -2 ) 
		{
			//TODO: throw exception?
			return;
		}
		
		List<int> spriteIndicesDisplayed = new List<int>();

		//pre-populate the list 
		for (int i = 0; i< slots.Count; i++)
		{
			spriteIndicesDisplayed.Add (selectedIndex);
		}

		if (spriteList.Count == 1) 
		{
			//When we have only one sprite, no math is required... it is the only one available to display
		} 
		else 
		{
			spriteIndicesDisplayed[masterSlotIndex] = selectedIndex;

			//this loop will go through all the slots to the right of the master slot (except the last slot which is only for off screen buffering)
			for (int i = masterSlotIndex+1; i < slots.Count; i++)
			{
				spriteIndicesDisplayed[i] = spriteIndicesDisplayed[i-1] + 1;
				if (spriteIndicesDisplayed[i] > spriteList.Count-1)
				{
					spriteIndicesDisplayed[i] = 0;
				}
			}

			//this loop will go through all the slots to the left of the master slot (except the last slot which is only for off screen buffering)

			for (int i = masterSlotIndex-1; i >= 0; i--)
			{
				spriteIndicesDisplayed[i] = spriteIndicesDisplayed[i+1] -1 ;
				if (spriteIndicesDisplayed[i] < 0)
				{
					spriteIndicesDisplayed[i] = spriteList.Count-1;
				}
			}

			//set the buffered slot's indices:
			//spriteIndicesDisplayed[0] = spriteIndicesDisplayed[spriteIndicesDisplayed.Count-2];
			//spriteIndicesDisplayed[spriteIndicesDisplayed.Count-1] = spriteIndicesDisplayed[1];
		}

		for (int i = 0; i<slots.Count; i++)
		{
			slotRenderers[i].sprite = spriteList[spriteIndicesDisplayed[i]];
		}

		Animate();
	}

	private void Animate()
	{
		//TODO: this is assuming the order of the slots in the list is from left to right on the screen
		if (ScrollingLeft) 
		{
			for (int i = slots.Count-1; i > 0; i--)
			{
				float newPosX = slots[i].transform.position.x - slotDistances[i-1].x/fps;
				float newPosY = slots[i].transform.position.y - slotDistances[i-1].y/fps;
				float newPosZ = slots[i].transform.position.z - slotDistances[i-1].z/fps;

				float newScaleX = slots[i].transform.localScale.x - slotDeltaScales[i-1].x/fps;
				float newScaleY = slots[i].transform.localScale.y - slotDeltaScales[i-1].y/fps;
				float newScaleZ = slots[i].transform.localScale.z - slotDeltaScales[i-1].z/fps;

				slots[i].transform.position = new Vector3 (newPosX, newPosY, newPosZ);
				slots[i].transform.localScale = new Vector3 (newScaleX, newScaleY, newScaleZ);

				//make sure we do not go over
				if (slots[i].transform.position.x <= slotOriginalPositions[i-1].x)
				{
					slots[i].transform.position = slotOriginalPositions[i-1];
					slots[i].transform.localScale = slotOriginalScales[i-1];
				}
			}

			//Check if we are done animating
			if (slots[1].transform.position == slotOriginalPositions[0])
			{
				//reset the control positions
				for (int i = 0; i<slots.Count; i++)
				{
					slots[i].transform.position = slotOriginalPositions[i];
					slots[i].transform.localScale = slotOriginalScales[i];
				}

				//reload the proper images
				selectedIndex++;
				if (selectedIndex >= spriteList.Count) {
					selectedIndex = 0;
				}
				
				ScrollingLeft = false;
			}
		} 
		else if (ScrollingRight) 
		{
			for (int i = 0; i < slots.Count-1; i++)
			{
				float newPosX = slots[i].transform.position.x + slotDistances[i].x/fps;
				float newPosY = slots[i].transform.position.y + slotDistances[i].y/fps;
				float newPosZ = slots[i].transform.position.z + slotDistances[i].z/fps;
				
				float newScaleX = slots[i].transform.localScale.x + slotDeltaScales[i].x/fps;
				float newScaleY = slots[i].transform.localScale.y + slotDeltaScales[i].y/fps;
				float newScaleZ = slots[i].transform.localScale.z + slotDeltaScales[i].z/fps;
				
				slots[i].transform.position = new Vector3 (newPosX, newPosY, newPosZ);
				slots[i].transform.localScale = new Vector3 (newScaleX, newScaleY, newScaleZ);
				
				//make sure we do not go over
				if (slots[i].transform.position.x >= slotOriginalPositions[i+1].x)
				{
					slots[i].transform.position = slotOriginalPositions[i+1];
					slots[i].transform.localScale = slotOriginalScales[i+1];
				}
			}
			
			//Check if we are done animating
			if (slots[0].transform.position == slotOriginalPositions[1])
			{
				//reset the control positions
				for (int i = 0; i<slots.Count; i++)
				{
					slots[i].transform.position = slotOriginalPositions[i];
					slots[i].transform.localScale = slotOriginalScales[i];
				}
				
				//reload the proper images
				selectedIndex--;
				if (selectedIndex < 0) {
					selectedIndex = spriteList.Count - 1;
				}
				
				ScrollingRight = false;
			}
		}

	}
}
