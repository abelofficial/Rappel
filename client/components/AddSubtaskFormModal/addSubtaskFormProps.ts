import { string, SchemaOf, object } from "yup";
import { CreateSubtaskCommand } from "../../types";

export const fieldLabels: Omit<CreateSubtaskCommand, "projectId"> = {
  title: "Title",
  description: "Description",
};

export const fieldNames: Omit<CreateSubtaskCommand, "projectId"> = {
  title: "title",
  description: "description",
};

export const initialValues: Omit<CreateSubtaskCommand, "projectId"> = {
  title: "",
  description: "",
};

export const validationSchema: SchemaOf<
  Omit<CreateSubtaskCommand, "projectId">
> = object({
  title: string().required("Title is required"),
  description: string()
    .required("Description is required")
    .min(20, "Description must be min 20 characters."),
});
