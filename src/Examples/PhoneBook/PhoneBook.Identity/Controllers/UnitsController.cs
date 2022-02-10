﻿namespace PhoneBook.Identity;

public class UnitsController : UnitsControllerBase<AppUnit>
{
    public UnitsController(IUnitService<AppUnit> unitService) : base(unitService)
    {
    }
}