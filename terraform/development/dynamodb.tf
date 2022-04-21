resource "aws_dynamodb_table" "FinancialSummaries_dynamodb_table" {
    name                  = "FinancialSummaries"
    billing_mode          = "PROVISIONED"
    read_capacity         = 100
    write_capacity        = 100
    hash_key              = "target_id"
    range_key             = "id"


    attribute {
        name              = "id"
        type              = "S"
    }

    attribute {
        name              = "target_id"
        type              = "S"
    }
	
    tags = {
        Name              = "financial-summary-api-${var.environment_name}"
        Environment       = var.environment_name
        terraform-managed = true
        project_name      = var.project_name
        BackupPolicy      = "Dev"
    }

    

    point_in_time_recovery {
        enabled           = true
    }
}
