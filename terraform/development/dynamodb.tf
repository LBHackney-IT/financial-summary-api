resource "aws_dynamodb_table" "FinancialSummaries_dynamodb_table" {
    name                  = "FinancialSummaries"
    billing_mode          = "PROVISIONED"
    read_capacity         = 10
    write_capacity        = 10
    hash_key              = "id"

    attribute {
        name              = "id"
        type              = "S"
    }
	
    attribute {
        name              = "summary_type"
        type              = "S"
    }
	
    attribute {
        name              = "target_id"
        type              = "S"
    }

    attribute {
        name              = "target_name"
        type              = "S"
    }

    tags = {
        Name              = "accounts-api-${var.environment_name}"
        Environment       = var.environment_name
        terraform-managed = true
        project_name      = var.project_name
    }

    global_secondary_index {
        name               = "summary_type_dx"
        hash_key           = "summary_type"
        write_capacity     = 10
        read_capacity      = 10
        projection_type    = "ALL"
    }

    global_secondary_index {
        name               = "target_id_dx"
        hash_key           = "target_id"
        write_capacity     = 10
        read_capacity      = 10
        projection_type    = "ALL"
    }

    global_secondary_index {
        name               = "target_name_dx"
        hash_key           = "target_name"
        write_capacity     = 10
        read_capacity      = 10
        projection_type    = "ALL"
    }

    point_in_time_recovery {
        enabled           = true
    }
}