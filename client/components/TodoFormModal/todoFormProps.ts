import { CreateSubtaskCommandDto } from "../../types";
import { string, SchemaOf, object } from "yup";

export const fieldLabels: CreateSubtaskCommandDto = {
  title: "Title",
  description: "Description",
};

export const fieldNames: CreateSubtaskCommandDto = {
  title: "title",
  description: "description",
};

export const initialValues: CreateSubtaskCommandDto = {
  title: "",
  description: "",
};

export const validationSchema: SchemaOf<CreateSubtaskCommandDto> = object({
  title: string().required("Title is required"),
  description: string()
    .required("Description is required")
    .min(20, "Description must be min 20 characters."),
});
