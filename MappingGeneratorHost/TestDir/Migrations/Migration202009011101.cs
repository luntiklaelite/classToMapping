//Generated by MigrationGenerator, 01.09.2020 11:01:11
using System.Data;
using Migrator.Framework;

namespace ITS.DbMigration.Bridges
{
	/// <summary>
	/// Миграция для создания схемы
	/// </summary>
	[Migration(202009011101)]
	public class Migration202009011101 : Migration
	{
		public override void Up()
		{
			Database.AddTable("bridges_bridge", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("construction", DbType.String),
					new Column("first_obstacle_name", DbType.String),
					new Column("second_obstacle_name", DbType.String),
					new Column("road_name", DbType.String),
					new Column("subject_code", DbType.Int16),
					new Column("territorial_road_control_code", DbType.Int32),
					new Column("road_code", DbType.String),
					new Column("kilometer", DbType.Int32),
					new Column("quantity_line_bridge", DbType.Int32),
					new Column("quantity_line_approach", DbType.Int32),
					new Column("markup", DbType.Boolean),
					new Column("nearest_village", DbType.String),
					new Column("distance_to_village", DbType.Single),
					new Column("charact_obstacle_b", DbType.Single),
					new Column("charact_obstacle_h", DbType.Single),
					new Column("charact_obstacle_v", DbType.Single),
					new Column("charact_obstacle_direction_of_flow", DbType.Boolean),
					new Column("underbridge_clearance", DbType.Single),
					new Column("length", DbType.Single),
					new Column("hole", DbType.Single),
					new Column("height_dimension", DbType.Single),
					new Column("width_dimension_b", DbType.Single),
					new Column("width_dimension_t1", DbType.Single),
					new Column("width_dimension_t2", DbType.Single),
					new Column("width_dimension_c", DbType.Single),
					new Column("width_dimension_c1", DbType.Single),
					new Column("width_dimension_c2", DbType.Single),
					new Column("width_dimension_вriveway_count", DbType.Int32),
					new Column("width_dimension_вriveway_width", DbType.Single),
					new Column("design_burden", DbType.String),
					new Column("longitude_scheme", DbType.String),
					new Column("oblique_angle", DbType.Single),
					new Column("slope_longitudinal", DbType.Single),
					new Column("slope_lateral", DbType.Single),
					new Column("railings_height", DbType.Single),
					new Column("adapter_plates_length", DbType.Single),
					new Column("road_signs_before", DbType.Boolean),
					new Column("road_signs_after", DbType.Boolean),
					new Column("info_of_reconstruction", DbType.String),
					new Column("info_of_repairs", DbType.String),
					new Column("security", DbType.Boolean),
					new Column("notes", DbType.String),
					new Column("num_line_road", DbType.String),
					new Column("first_obstacle_type", DbType.Byte, 0),
					new Column("second_obstacle_type", DbType.Byte, 0),
					new Column("road_category", DbType.Byte, 0),
					new Column("construction_date", DbType.Date),
					new Column("reconstruction_date", DbType.Date),
					new Column("repairs_date", DbType.Date),
					new Column("location_in_plan", DbType.Byte, 0),
					new Column("cover_type", DbType.Byte, 0),
					new Column("drainage_type", DbType.Byte, 0),
					new Column("expansion_joints_type", DbType.Byte, 0),
					new Column("protection_on_bridge_id", DbType.Int64),
					new Column("protection_on_approach_id", DbType.Int64),
					new Column("sidewalks", DbType.Byte, 0),
					new Column("railings", DbType.Byte, 0),
					new Column("start_bridge_side_id", DbType.Int64),
					new Column("end_bridge_side_id", DbType.Int64),
					new Column("regulatory_structures", DbType.Byte, 0),
					new Column("cones_strengthening", DbType.Byte, 0),
					new Column("adapter_plates_id", DbType.Int64),
					new Column("project_organization_id", DbType.Int64),
					new Column("building_company_id", DbType.Int64),
					new Column("exploit_organization_id", DbType.Int64),
					new Column("communications", DbType.Byte, 0),
					new Column("arrangements", DbType.Byte, 0),
					new Column("date_view", DbType.Date),
					new Column("supports_id", DbType.Int64),
					new Column("span_structures_id", DbType.Int64),
					new Column("status", DbType.Byte, 0),
					new Column("feature_object_id", DbType.Int64),
					new Column("quality_bridge_type", DbType.Byte, 0),
					new Column("construction_type", DbType.Byte, 0),
					new Column("move_type", DbType.Byte, 0),
				});
			Database.AddTable("bridges_bridge_side", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("carriageway_width", DbType.Single),
					new Column("longitudinal_slope", DbType.Single),
					new Column("embankment_height", DbType.Single),
				});
			Database.AddTable("bridges_bridge_support", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("support_type_additional_info", DbType.String),
					new Column("heights", DbType.Binary),
					new Column("laying_depth", DbType.Single),
					new Column("massive_part_size_along", DbType.Single),
					new Column("massive_part_size_across", DbType.Single),
					new Column("pile_count", DbType.Int32),
					new Column("max_distance_between_axis", DbType.Single),
					new Column("scheme_k_left", DbType.Single),
					new Column("scheme_k_right", DbType.Single),
					new Column("scheme_pile_distances", DbType.Binary),
					new Column("scheme_pile_row_distance1", DbType.Single),
					new Column("scheme_pile_row_distance2", DbType.Single),
					new Column("crossbar_width", DbType.Single),
					new Column("crossbar_height", DbType.Single),
					new Column("crossbar_length", DbType.Single),
					new Column("pile_cross_section_width", DbType.Single),
					new Column("pile_cross_section_height", DbType.Single),
					new Column("notes", DbType.String),
					new Column("support_type", DbType.Byte, 0),
					new Column("foundation_type", DbType.Byte, 0),
					new Column("material_id", DbType.Int64),
					new Column("typical_project_id", DbType.Int64),
				});
			Database.AddTable("bridges_material", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("name", DbType.String),
				});
			Database.AddTable("bridges_protection", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("height", DbType.Single),
					new Column("type", DbType.Byte, 0),
				});
			Database.AddTable("bridges_span_pile", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("count_in_span", DbType.Int32),
					new Column("height", DbType.Single),
					new Column("roadway_cover_material_id", DbType.Int64),
				});
			Database.AddTable("bridges_span_structure", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("cross_scheme_ka", DbType.Single),
					new Column("cross_scheme_kb", DbType.Single),
					new Column("cross_scheme_pile_distances", DbType.Binary),
					new Column("plate_thikness", DbType.Single),
					new Column("roadway_clothing_thikness", DbType.Single),
					new Column("roadway_clothing_add_layer_thikness", DbType.Single),
					new Column("roadway_clothing_add_layer_weight", DbType.Single),
					new Column("main_pile_count", DbType.Int32),
					new Column("main_pile_height_span", DbType.Single),
					new Column("main_pile_height_support", DbType.Single),
					new Column("main_pile_thikness_edge", DbType.Single),
					new Column("additional_linear_load", DbType.String),
					new Column("notes", DbType.String),
					new Column("system", DbType.Byte, 0),
					new Column("span_structure_type", DbType.Byte, 0),
					new Column("construction_roadway", DbType.Byte, 0),
					new Column("material_id", DbType.Int64),
					new Column("construction_year", DbType.Date),
					new Column("typical_project_id", DbType.Int64),
					new Column("span_type_movable", DbType.Byte, 0),
					new Column("span_type_motionless", DbType.Byte, 0),
					new Column("joint_type", DbType.Byte, 0),
					new Column("cross_join", DbType.Byte, 0),
					new Column("plate_material_id", DbType.Int64),
					new Column("cross_pile_id", DbType.Int64),
					new Column("longitudinal_pile_id", DbType.Int64),
				});
			Database.AddTable("bridges_typical_project", new[]
				{
					new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),
					new Column("name", DbType.String),
				});

			Database.AddForeignKey("bridge_to_protection_0", "bridges_bridge", "protection_on_bridge_id", "bridges_protection", "id");
			Database.AddForeignKey("bridge_to_protection_1", "bridges_bridge", "protection_on_approach_id", "bridges_protection", "id");
			Database.AddForeignKey("bridge_to_bridge_side_2", "bridges_bridge", "start_bridge_side_id", "bridges_bridge_side", "id");
			Database.AddForeignKey("bridge_to_bridge_side_3", "bridges_bridge", "end_bridge_side_id", "bridges_bridge_side", "id");
			//Database.AddForeignKey("bridges_bridge", "bridge_to_adapter_plates_availability_4", "bridges_bridge", "adapter_plates_id", "adapter_plates_availability", "id");
			Database.AddForeignKey("bridge_to_organization_5", "bridges_bridge", "project_organization_id", "info_organization", "id");
			Database.AddForeignKey("bridge_to_organization_6", "bridges_bridge", "building_company_id", "info_organization", "id");
			Database.AddForeignKey("bridge_to_organization_7", "bridges_bridge", "exploit_organization_id", "info_organization", "id");
			//Database.AddForeignKey("bridges_bridge", "bridge_to_i_list<_bridge_support>_8", "bridges_bridge", "supports_id", "i_list<_bridge_support>", "id");
			//Database.AddForeignKey("bridges_bridge", "bridge_to_i_list<_span_structure>_9", "bridges_bridge", "span_structures_id", "i_list<_span_structure>", "id");
			Database.AddForeignKey("bridge_to_feature_object_10", "bridges_bridge", "feature_object_id", "featureobject", "id");
			Database.AddForeignKey("bridge_support_to_material_0", "bridges_bridge_support", "material_id", "bridges_material", "id");
			Database.AddForeignKey("bridge_support_to_typical_project_1", "bridges_bridge_support", "typical_project_id", "bridges_typical_project", "id");
			Database.AddForeignKey("span_pile_to_material_0", "bridges_span_pile", "roadway_cover_material_id", "bridges_material", "id");
			Database.AddForeignKey("span_structure_to_material_0", "bridges_span_structure", "material_id", "bridges_material", "id");
			Database.AddForeignKey("span_structure_to_typical_project_1", "bridges_span_structure", "typical_project_id", "bridges_typical_project", "id");
			Database.AddForeignKey("span_structure_to_material_2", "bridges_span_structure", "plate_material_id", "bridges_material", "id");
			Database.AddForeignKey("span_structure_to_span_pile_3", "bridges_span_structure", "cross_pile_id", "bridges_span_pile", "id");
			Database.AddForeignKey("span_structure_to_span_pile_4", "bridges_span_structure", "longitudinal_pile_id", "bridges_span_pile", "id");
		}

		public override void Down()
		{
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_protection_0");
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_protection_1");
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_bridge_side_2");
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_bridge_side_3");
			//Database.RemoveForeignKey("bridges_bridge", "bridge_to_adapter_plates_availability_4");
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_organization_5");
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_organization_6");
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_organization_7");
			//Database.RemoveForeignKey("bridges_bridge", "bridge_to_i_list<_bridge_support>_8");
			//Database.RemoveForeignKey("bridges_bridge", "bridge_to_i_list<_span_structure>_9");
			Database.RemoveForeignKey("bridges_bridge", "bridge_to_feature_object_10");
			Database.RemoveForeignKey("bridges_bridge_support", "bridge_support_to_material_0");
			Database.RemoveForeignKey("bridges_bridge_support", "bridge_support_to_typical_project_1");
			Database.RemoveForeignKey("bridges_span_pile", "span_pile_to_material_0");
			Database.RemoveForeignKey("bridges_span_structure", "span_structure_to_material_0");
			Database.RemoveForeignKey("bridges_span_structure", "span_structure_to_typical_project_1");
			Database.RemoveForeignKey("bridges_span_structure", "span_structure_to_material_2");
			Database.RemoveForeignKey("bridges_span_structure", "span_structure_to_span_pile_3");
			Database.RemoveForeignKey("bridges_span_structure", "span_structure_to_span_pile_4");

			Database.RemoveTable("bridges_bridge");
			Database.RemoveTable("bridges_bridge_side");
			Database.RemoveTable("bridges_bridge_support");
			Database.RemoveTable("bridges_material");
			Database.RemoveTable("bridges_protection");
			Database.RemoveTable("bridges_span_pile");
			Database.RemoveTable("bridges_span_structure");
			Database.RemoveTable("bridges_typical_project");
		}
	}
}
