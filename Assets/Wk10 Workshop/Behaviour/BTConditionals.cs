using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HasHammer : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && character.hasHammer)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class NeedsHammer : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && !character.hasHammer)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class HasDrill : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && character.hasDrill)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class NeedsDrill : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && !character.hasDrill)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class HasMoney : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && character.hasMoney)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class NeedsMoney : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && !character.hasMoney)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class BrokeDoor : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && character.brokeDoor)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class LockedDoor : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && !character.brokeDoor)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class OpenedVault : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && character.openedVault)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class LockedVault : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && !character.openedVault)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class OnTheJob : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && !character.successfullyEscaped)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

public class SuccessfullyEscaped : Conditional
{
    public SOPDCharacter character;
    
    public override TaskStatus OnUpdate()
    {
        if (character && character.successfullyEscaped)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}