using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollingItemMenu : MonoBehaviour 
{
	public GameObject slot1;
	public GameObject slot2;
	public GameObject slot3;
	private GameObject slotHiddenRight;
	private GameObject slotHiddenLeft;

	public List<Sprite> spriteList;
	public List<string> values;
	public int selectedIndex = 0;

	SpriteRenderer slot1SpriteRenderer;
	SpriteRenderer slot2SpriteRenderer;
	SpriteRenderer slot3SpriteRenderer;
	SpriteRenderer slotHiddenRightSpriteRenderer;
	SpriteRenderer slotHiddenLeftSpriteRenderer;

	private bool ScrollingRight = false;
	private bool ScrollingLeft = false;

	private Vector3 slot1OriginalPosition;
	private Vector3 slot2OriginalPosition;
	private Vector3 slot3OriginalPosition;
	private Vector3 slotHiddenRightOriginalPosition;
	private Vector3 slotHiddenLeftOriginalPosition;

	private Vector3 slot1OriginalScale;
	private Vector3 slot2OriginalScale;
	private Vector3 slot3OriginalScale;
	private Vector3 slotHiddenRightOriginalScale;
	private Vector3 slotHiddenLeftOriginalScale;

	private float deltaDistance;
	
	private float slot1deltaScale;
	private float slot2deltaScale;
	//no scaling for all other transitions

	public string getSelectedValue()
	{
		return values [selectedIndex];
	}
	// Use this for initialization
	void Start () {
		//Must always have at least 3 items and the same number of sprites + values

		if (spriteList.Count != values.Count) 
		{
			//ERROR
		}

		if (spriteList.Count < 3) 
		{
			//ERROR
		}

		slot1SpriteRenderer = slot1.GetComponent<SpriteRenderer> ();
		slot2SpriteRenderer = slot2.GetComponent<SpriteRenderer> ();
		slot3SpriteRenderer = slot3.GetComponent<SpriteRenderer> ();

		slot1OriginalPosition = new Vector3( slot1.transform.position.x, slot1.transform.position.y, 1);
		slot2OriginalPosition = new Vector3( slot2.transform.position.x, slot2.transform.position.y, 1);
		slot3OriginalPosition = new Vector3( slot3.transform.position.x, slot3.transform.position.y, 1);

		slot1OriginalScale = new Vector3( slot1.transform.localScale.x, slot1.transform.localScale.y, 1);
		slot2OriginalScale = new Vector3( slot2.transform.localScale.x, slot2.transform.localScale.y, 1);
		slot3OriginalScale = new Vector3( slot3.transform.localScale.x, slot3.transform.localScale.y, 1);

		deltaDistance = slot2.transform.position.x - slot1.transform.position.x;

		slot1deltaScale = slot2.transform.localScale.x - slot1.transform.localScale.x;
		slot2deltaScale = slot3.transform.localScale.x - slot2.transform.localScale.x;

		//create the hidden Left Slot
		slotHiddenLeft = new GameObject ();
		slotHiddenLeft.transform.position = new Vector3 (slot1OriginalPosition.x - deltaDistance, slot1OriginalPosition.y, 1);
		slotHiddenLeft.AddComponent<SpriteRenderer> ();
		slotHiddenLeftSpriteRenderer = slotHiddenLeft.GetComponent<SpriteRenderer> ();
		slotHiddenLeftOriginalPosition = new Vector3( slotHiddenLeft.transform.position.x, slotHiddenLeft.transform.position.y, 1); 
		slotHiddenLeftOriginalScale = new Vector3( slotHiddenLeft.transform.localScale.x, slotHiddenLeft.transform.localScale.y, 1);

		//create the hidden Right Slot
		slotHiddenRight = new GameObject ();
		slotHiddenRight.transform.position = new Vector3 (slot3OriginalPosition.x + deltaDistance, slot3OriginalPosition.y, 1);
		slotHiddenRight.AddComponent<SpriteRenderer> ();
		slotHiddenRightSpriteRenderer = slotHiddenRight.GetComponent<SpriteRenderer> ();
		slotHiddenRightOriginalPosition = new Vector3( slotHiddenRight.transform.position.x, slotHiddenRight.transform.position.y, 1); 
		slotHiddenRightOriginalScale = new Vector3( slotHiddenRight.transform.localScale.x, slotHiddenRight.transform.localScale.y, 1);
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
		int fps = 25;

		//calculate indices to display
		int leftleftIndex;
		leftleftIndex = selectedIndex - 2;
		if (leftleftIndex < 0 )
		{
			leftleftIndex = spriteList.Count + selectedIndex- 2;
		}

		int leftIndex = selectedIndex - 1;
		if (leftIndex < 0) 
		{
			leftIndex = spriteList.Count - 1;
		}
		
		int centerIndex = selectedIndex;
		
		int rightIndex = selectedIndex +1;
		if (rightIndex >= spriteList.Count) 
		{
			rightIndex = 0;
		}

		int rightrightIndex = selectedIndex + 2;
		if (rightrightIndex >= spriteList.Count)
		{
			rightrightIndex = selectedIndex - spriteList.Count + 2;
		}

		slotHiddenLeftSpriteRenderer.sprite = spriteList [leftleftIndex];
		slot1SpriteRenderer.sprite = spriteList[leftIndex];
		slot2SpriteRenderer.sprite = spriteList[centerIndex];
		slot3SpriteRenderer.sprite = spriteList[rightIndex];
		slotHiddenRightSpriteRenderer.sprite = spriteList [rightrightIndex];

		//Animation
		//TODO: this is assuming a constant "Y" value for all slots
		//TODO: this is assuming a constant spacing between all slots
		//TODO: this is assuming the following Postition on the UI:  Slot1, Slot2, Slot3
		//TODO: this is assuming slot 2 is scaled bigger than slot1 and 3,
		//TODO: this is assuming slot1 and 3 are the same size
		//TODO: this is assuming the X and Y scaling are the same for all slots
		if (ScrollingRight) 
		{
			//hidden slot left moves right
			if (slotHiddenLeft.transform.position.x < slot1OriginalPosition.x)
			{
				//adjust Size
				float newX = slotHiddenLeft.transform.position.x + deltaDistance/fps;
				float newY = slotHiddenLeft.transform.position.y;
				slotHiddenLeft.transform.position = new Vector3(newX,newY,1);
				if (slotHiddenLeft.transform.position.x > slot1OriginalPosition.x)
				{
					slotHiddenLeft.transform.position = slot1OriginalPosition;
				}
				
				//no scaling required
			}

			//slot 1 moves right
			if (slot1.transform.position.x < slot2OriginalPosition.x)
			{
				//adjust Size
				float newX = slot1.transform.position.x + deltaDistance/fps;
				float newY = slot1.transform.position.y;
				slot1.transform.position = new Vector3(newX,newY,1);
				if (slot1.transform.position.x > slot2OriginalPosition.x)
				{
					slot1.transform.position = slot2OriginalPosition;
				}

				//adjust Scale
				newX = slot1.transform.localScale.x + slot1deltaScale/fps;
				newY = slot1.transform.localScale.y + slot1deltaScale/fps;
				slot1.transform.localScale = new Vector3(newX,newY,1);

				if (slot1.transform.localScale.x > slot2OriginalScale.x)
				{
					slot1.transform.localScale = slot2OriginalScale;
				}
			}

			//slot 2 moves right
			if (slot2.transform.position.x < slot3OriginalPosition.x)
			{
				//adjust Size
				float newX = slot2.transform.position.x + deltaDistance/fps;
				float newY = slot2.transform.position.y;
				slot2.transform.position = new Vector3(newX,newY,1);
				if (slot2.transform.position.x > slot3OriginalPosition.x)
				{
					slot2.transform.position = slot3OriginalPosition;
				}

				//adjust Scale
				newX = slot2.transform.localScale.x + slot2deltaScale/fps;
				newY = slot2.transform.localScale.y + slot2deltaScale/fps;
				slot2.transform.localScale = new Vector3(newX,newY,1);
				
				if (slot2.transform.localScale.x < slot3OriginalScale.x)
				{
					slot2.transform.localScale = slot3OriginalScale;
				}
			}

			//slot3 moves off the screen
			if (slot3.transform.position.x < slotHiddenRightOriginalPosition.x)
			{
				//adjust Size
				float newX = slot3.transform.position.x + deltaDistance/fps;
				float newY = slot3.transform.position.y;
				slot3.transform.position = new Vector3(newX,newY,1);
				if (slot3.transform.position.x > slotHiddenRightOriginalPosition.x)
				{
					slot3.transform.position = slotHiddenRightOriginalPosition;
				}
				
				//no scaling required
			}

			if (slot1.transform.position == slot2OriginalPosition
			    && slot1.transform.localScale == slot2OriginalScale)
			    //Note: Don't care about comparing the others... they all move at the same speed anyways
			{
				//reset the control positions
				slotHiddenLeft.transform.position = slotHiddenLeftOriginalPosition;
				slot1.transform.position = slot1OriginalPosition;
				slot2.transform.position = slot2OriginalPosition;
				slot3.transform.position = slot3OriginalPosition;
				slotHiddenRight.transform.position = slotHiddenRightOriginalPosition;

				slotHiddenLeft.transform.localScale = slotHiddenLeftOriginalScale;
				slot1.transform.localScale = slot1OriginalScale;
				slot2.transform.localScale = slot2OriginalScale;
				slot3.transform.localScale = slot3OriginalScale;
				slotHiddenRight.transform.localScale = slotHiddenRightOriginalScale;

				//reload the proper images
				selectedIndex--;
				if (selectedIndex < 0) 
				{
					selectedIndex = spriteList.Count-1;
				}

				ScrollingRight = false;
			}
		}
		else if (ScrollingLeft) 
		{

			//slot 1 moves off the screen
			if (slot1.transform.position.x > slotHiddenLeftOriginalPosition.x)
			{
				//adjust Size
				float newX = slot1.transform.position.x - deltaDistance/fps;
				float newY = slot1.transform.position.y;
				slot1.transform.position = new Vector3(newX,newY,1);
				if (slot1.transform.position.x < slotHiddenLeftOriginalPosition.x)
				{
					slot1.transform.position = slotHiddenLeftOriginalPosition;
				}
				
				//no scaling required
			}
			
			//slot 2 moves left
			if (slot2.transform.position.x > slot1OriginalPosition.x)
			{
				//adjust Size
				float newX = slot2.transform.position.x - deltaDistance/fps;
				float newY = slot2.transform.position.y;
				slot2.transform.position = new Vector3(newX,newY,1);
				if (slot2.transform.position.x < slot1OriginalPosition.x)
				{
					slot2.transform.position = slot1OriginalPosition;
				}
				
				//adjust Scale
				newX = slot2.transform.localScale.x + slot2deltaScale/fps;
				newY = slot2.transform.localScale.y + slot2deltaScale/fps;
				slot2.transform.localScale = new Vector3(newX,newY,1);
				
				if (slot2.transform.localScale.x < slot1OriginalScale.x)
				{
					slot2.transform.localScale = slot1OriginalScale;
				}
			}
			
			//slot3 moves left
			if (slot3.transform.position.x > slot2OriginalPosition.x)
			{
				//adjust Size
				float newX = slot3.transform.position.x - deltaDistance/fps;
				float newY = slot3.transform.position.y;
				slot3.transform.position = new Vector3(newX,newY,1);
				if (slot3.transform.position.x < slot2OriginalPosition.x)
				{
					slot3.transform.position = slot2OriginalPosition;
				}
				
				//adjust Scale
				newX = slot3.transform.localScale.x + slot1deltaScale/fps;//same as slot1... no need for another variable
				newY = slot3.transform.localScale.y + slot1deltaScale/fps;//same as slot1... no need for another variable
				slot3.transform.localScale = new Vector3(newX,newY,1);
				
				if (slot3.transform.localScale.x > slot2OriginalScale.x)
				{
					slot3.transform.localScale = slot2OriginalScale;
				}
			}
			
			//slot 1 moves off the screen
			if (slotHiddenRight.transform.position.x > slot3OriginalPosition.x)
			{
				//adjust Size
				float newX = slotHiddenRight.transform.position.x - deltaDistance/fps;
				float newY = slotHiddenRight.transform.position.y;
				slotHiddenRight.transform.position = new Vector3(newX,newY,1);
				if (slotHiddenRight.transform.position.x < slot3OriginalPosition.x)
				{
					slotHiddenRight.transform.position = slot3OriginalPosition;
				}
				
				//no scaling required
			}
			
			if (slot2.transform.position == slot1OriginalPosition
			    && slot2.transform.localScale == slot1OriginalScale)
				//Note: Don't care about comparing the others... they all move at the same speed anyways
			{
				//reset the control positions
				slotHiddenLeft.transform.position = slotHiddenLeftOriginalPosition;
				slot1.transform.position = slot1OriginalPosition;
				slot2.transform.position = slot2OriginalPosition;
				slot3.transform.position = slot3OriginalPosition;
				slotHiddenRight.transform.position = slotHiddenRightOriginalPosition;
				
				slotHiddenLeft.transform.localScale = slotHiddenLeftOriginalScale;
				slot1.transform.localScale = slot1OriginalScale;
				slot2.transform.localScale = slot2OriginalScale;
				slot3.transform.localScale = slot3OriginalScale;
				slotHiddenRight.transform.localScale = slotHiddenRightOriginalScale;
				
				//reload the proper images
				selectedIndex++;
				if (selectedIndex >= spriteList.Count) 
				{
					selectedIndex = 0;
				}
				
				ScrollingLeft = false;
			}
		}

	}
}
