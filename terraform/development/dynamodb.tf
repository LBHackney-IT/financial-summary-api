resource "aws_dynamodb_table" "FinancialSummaries_dynamodb_table" {
    name                  = "FinancialSummaries"
    billing_mode          = "PROVISIONED"
    read_capacity         = 10
    write_capacity        = 10
    hash_key              = "pk"
    range_key             = "id"


     attribute {
        name              = "pk"
        type              = "S"
    }

    attribute {
        name              = "id"
        type              = "S"
    }
	
    tags = {
        Name              = "accounts-api-${var.environment_name}"
        Environment       = var.environment_name
        terraform-managed = true
        project_name      = var.project_name
    }

    

    point_in_time_recovery {
        enabled           = true
    }
}
