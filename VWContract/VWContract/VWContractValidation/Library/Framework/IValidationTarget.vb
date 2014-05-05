Public Interface IValidationTarget
    Sub CheckRules()
    Sub Requirement(previousData As Object, validationRoot As Rule)
End Interface
